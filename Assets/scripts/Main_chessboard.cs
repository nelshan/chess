using System;
using System.Collections.Generic;
using UnityEngine;


public class Main_chessboard : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material tileMaterial;
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Material highlightMaterial;

    [Header("Align Generated Tiles with chess board Model")]
    [SerializeField] private float tileSize = 0.99f;
    [SerializeField] private float yoffset = 1.01f;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;

    [Header("Prefabs and Materials for chess pieces")]
    [SerializeField] private GameObject[] Prefabs;
    [SerializeField] private Material[] teamMaterials;

    [Header("Constant value for chesspiece when distroy")]
    [SerializeField] private float Dead_ChessSize = 0.3f;
    [SerializeField] private float DeadSpacing = 0.3f;

    [Header("Logic")]
    // Constants
    private const int Tile_Count_X  = 8;// Define the dimensions of the chessboard
    private const int Tile_Count_Y  = 8;// Define the dimensions of the chessboard

    
    // References
    private GameObject[,] tiles; // 2D array to hold references to all tiles on the chessboard
    private Camera currentCamera; // Reference to the current camera in the scene
    private Vector2Int currentMouse_Hover;
    private Vector3 bounds;
    private ChessPiece[,] Active_chessPieces;//2D array to hold references of all the active ChessPiece on the chessboard
    private ChessPiece Current_ChessPiece_dragging;
    private List<ChessPiece> DeadWhite = new List<ChessPiece>();//this will store all the white chesspieces that is distroy my black chesspieces
    private List<ChessPiece> DeadBlack = new List<ChessPiece>();//this will store all the black chesspieces that is distroy my white chesspieces
    private List<Vector2Int> Avaiable_ChessMoves = new List<Vector2Int>();



    private void Awake()
    {
        // Generate all the tiles on the chessboard
        GenerateAllTiles(tileSize, Tile_Count_X , Tile_Count_Y );

        //SpawnSinglePices(ChessPieceType.King, 0);
        SpawnAllPices();

        PositionAll_ChessPieces();
    }

    private void Update()
    {
        // Ensure the current camera is set
        if (!currentCamera)
        {
            currentCamera = Camera.main;
            return;
        }

        // Handle tile selection when the player clicks on a tile
        RaycastHit RayCastHit_Info;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RayCastHit_Info, 100, LayerMask.GetMask("Tile", "Hover", "Highlight")))//as raycast will hit all the tile that has layer name"Tile" based on input of mousePosition
        {
            // Handle tile selection logic here
            Vector2Int hitPosition = LookupTileIndex(RayCastHit_Info.transform.gameObject);

            //this loop is for "if the mouse is already hovering a tile, after not hovering any tiles"
            if (currentMouse_Hover == -Vector2Int.one)
            {
                currentMouse_Hover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                tiles[hitPosition.x, hitPosition.y].GetComponent<MeshRenderer>().material = hoverMaterial;
            }

            //this loop is for "if the mouse is already hovering a tile, change to privious one"
            if (currentMouse_Hover != hitPosition)
            {
                tiles[currentMouse_Hover.x, currentMouse_Hover.y].layer = LayerMask.NameToLayer("Tile");
                tiles[currentMouse_Hover.x, currentMouse_Hover.y].GetComponent<MeshRenderer>().material = tileMaterial;
                currentMouse_Hover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                tiles[hitPosition.x, hitPosition.y].GetComponent<MeshRenderer>().material = hoverMaterial;
            }

            //if left mouse button is pressing down
            if (Input.GetMouseButtonDown(0))
            {
                if (Active_chessPieces[hitPosition.x, hitPosition.y] != null)
                {
                    //is it our turn
                    if (true)
                    {
                        Current_ChessPiece_dragging = Active_chessPieces[hitPosition.x, hitPosition.y];
                        
                        //get a list of Avaiable ChessMoves of the given chesspieces
                        Avaiable_ChessMoves = Current_ChessPiece_dragging.GetAvaiable_ChessMove(ref Active_chessPieces, Tile_Count_X, Tile_Count_Y);
                        HighlightTiles();
                    }
                }
            }

            //if left mouse button is release
            if (Current_ChessPiece_dragging != null && Input.GetMouseButtonUp(0))
            {
                Vector2Int previous_ChessPosition = new Vector2Int(Current_ChessPiece_dragging.currentX, Current_ChessPiece_dragging.currentY); 
                
                bool Valid_ChessMove = MoveTo(Current_ChessPiece_dragging, hitPosition.x, hitPosition.y);
                if (!Valid_ChessMove)
                {
                    Current_ChessPiece_dragging.SetPosition(GetTileCenter(previous_ChessPosition.x, previous_ChessPosition.y));
                    //Debug.Log("put can't move sound here");
                }

                // Unhighlight tiles after moving
                Remove_HighlightTiles();

                Current_ChessPiece_dragging = null;
            }
        }
        else
        {
            //this loop is for "when mouse crosshair goes outside of chess board"
            if (currentMouse_Hover != -Vector2Int.one)
            {
                tiles[currentMouse_Hover.x, currentMouse_Hover.y].layer = LayerMask.NameToLayer("Tile");
                currentMouse_Hover = -Vector2Int.one;
                Debug.Log("raycast and mouse crosshair is out of chessboard");
            }

            if (Current_ChessPiece_dragging && Input.GetMouseButtonUp(0))
            {
                Current_ChessPiece_dragging.SetPosition(GetTileCenter(Current_ChessPiece_dragging.currentX, Current_ChessPiece_dragging.currentY));
                Current_ChessPiece_dragging = null;
                Remove_HighlightTiles();
            }
        }
        /*/if we clicked the chesspiece
        if (Current_ChessPiece_dragging)
        {
            Plane horizontalPlane = new Plane(Vector3.up, Vector3.up * yoffset);
            float distant = 0.0f;
            if (horizontalPlane.Raycast(ray, out distant))
            {
                Current_ChessPiece_dragging.SetPosition(ray.GetPoint(distant) + Vector3.up * dragOffset);
            }
        }*/
    }

    //HighlightTiles and Remove_HighlightTiles
    private void HighlightTiles()
    {
        foreach (var tileIndex in Avaiable_ChessMoves)
        {
            tiles[tileIndex.x, tileIndex.y].layer = LayerMask.NameToLayer("Highlight");
            tiles[tileIndex.x, tileIndex.y].GetComponent<MeshRenderer>().material = highlightMaterial;

            // Check if the mouse is currently hovering over this tile
            if (currentMouse_Hover == tileIndex)
            {
                tiles[tileIndex.x, tileIndex.y].GetComponent<MeshRenderer>().material = hoverMaterial;
            }
        }
    }
    private void Remove_HighlightTiles()
    {
        foreach (var tileIndex in Avaiable_ChessMoves)
        {
            tiles[tileIndex.x, tileIndex.y].layer = LayerMask.NameToLayer("Tile");
            tiles[tileIndex.x, tileIndex.y].GetComponent<MeshRenderer>().material = tileMaterial;
        }
        Avaiable_ChessMoves.Clear();
    }
    /*private void HighlightTiles()
    {
        for (int i = 0; i < Avaiable_ChessMoves.Count; i++)
        {
            tiles[Avaiable_ChessMoves[i].x, Avaiable_ChessMoves[i].y].layer = LayerMask.NameToLayer("Highlight");
            tiles[Avaiable_ChessMoves[i].x, Avaiable_ChessMoves[i].y].GetComponent<MeshRenderer>().material = highlightMaterial;
        }
    }
    private void Remove_HighlightTiles()
    {
        for (int i = 0; i < Avaiable_ChessMoves.Count; i++)
        {
            int x = Avaiable_ChessMoves[i].x;
            int y = Avaiable_ChessMoves[i].y;
            int a = currentMouse_Hover.x;
            int b = currentMouse_Hover.y;

            tiles[x, y].layer = LayerMask.NameToLayer("Tile");
            tiles[a, b].GetComponent<MeshRenderer>().material = tileMaterial;
        
        }
    }*/

    // Generate all the tiles on the chessboard
    private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY)
    {
        yoffset += transform.position.y;
        bounds = new Vector3 (tileCountX/2 * tileSize, 0, tileCountX/2 * tileSize) + boardCenter;

        tiles = new GameObject[tileCountX, tileCountY]; // Initialize the tiles array with the correct dimensions
        for (int x = 0; x < tileCountX; x++)
        {
            for (int y = 0; y < tileCountY; y++)
            {
                // Generate and store a reference to each individual tile
                tiles[x, y] = GenerateSingleTile(tileSize, x, y);
            }
        }
    }
    // Generate a single tile on the chessboard
    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        // Create a new game object to represent the tile
        GameObject tileObject = new GameObject($"Tile [{x}, {y}]");
        tileObject.transform.parent = transform; // Set the chessboard as the parent of the tile object

        // Add a mesh renderer and set the tile material
        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

        // Calculate vertices for the tile
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, yoffset, y * tileSize) - bounds;
        vertices[1] = new Vector3(x * tileSize, yoffset, (y + 1) * tileSize) - bounds;
        vertices[2] = new Vector3((x + 1) * tileSize, yoffset, y * tileSize) - bounds;
        vertices[3] = new Vector3((x + 1) * tileSize, yoffset, (y + 1) * tileSize) - bounds;

        // Set triangles for the tile mesh
        int[] trianglesArray = new int[] { 0, 1, 2, 1, 3, 2 };

        // Assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = trianglesArray;
        mesh.RecalculateNormals();

        // Set the layer of the tile and add a collider for interaction
        tileObject.layer = LayerMask.NameToLayer("Tile");
        tileObject.AddComponent<BoxCollider>();

        return tileObject; // Return the generated tile object
    }
    //spawning chess pieces on chessboard
    private void SpawnAllPices()
    {
        Active_chessPieces = new ChessPiece[Tile_Count_X, Tile_Count_Y];

        int whiteTeam = 0;
        int blackTeam = 1;

        //spawn point of white team
        Active_chessPieces[0, 0] =SpawnSinglePices(ChessPieceType.Rook, whiteTeam);
        Active_chessPieces[1, 0] =SpawnSinglePices(ChessPieceType.Knight, whiteTeam);
        Active_chessPieces[2, 0] =SpawnSinglePices(ChessPieceType.Bishop, whiteTeam);
        Active_chessPieces[3, 0] =SpawnSinglePices(ChessPieceType.Queen, whiteTeam);
        Active_chessPieces[4, 0] =SpawnSinglePices(ChessPieceType.King, whiteTeam);
        Active_chessPieces[5, 0] =SpawnSinglePices(ChessPieceType.Bishop, whiteTeam);
        Active_chessPieces[6, 0] =SpawnSinglePices(ChessPieceType.Knight, whiteTeam);
        Active_chessPieces[7, 0] =SpawnSinglePices(ChessPieceType.Rook, whiteTeam);
        for (int pawn=0; pawn<Tile_Count_X; pawn++)
        {
            Active_chessPieces[pawn, 1] =SpawnSinglePices(ChessPieceType.Pawn, whiteTeam);
        }

        //spawn point of black team
        Active_chessPieces[0, 7] =SpawnSinglePices(ChessPieceType.Rook, blackTeam);
        Active_chessPieces[1, 7] =SpawnSinglePices(ChessPieceType.Knight, blackTeam);
        Active_chessPieces[2, 7] =SpawnSinglePices(ChessPieceType.Bishop, blackTeam);
        Active_chessPieces[3, 7] =SpawnSinglePices(ChessPieceType.Queen, blackTeam);
        Active_chessPieces[4, 7] =SpawnSinglePices(ChessPieceType.King, blackTeam);
        Active_chessPieces[5, 7] =SpawnSinglePices(ChessPieceType.Bishop, blackTeam);
        Active_chessPieces[6, 7] =SpawnSinglePices(ChessPieceType.Knight, blackTeam);
        Active_chessPieces[7, 7] =SpawnSinglePices(ChessPieceType.Rook, blackTeam);
        for (int pawn=0; pawn<Tile_Count_X; pawn++)
        {
            Active_chessPieces[pawn, 6] =SpawnSinglePices(ChessPieceType.Pawn, blackTeam);
        }

    }
    private ChessPiece SpawnSinglePices(ChessPieceType type, int team)
    {
        ChessPiece ChessPieceType_reference = Instantiate(Prefabs[(int)type - 1], transform).GetComponent<ChessPiece>();
        
        ChessPieceType_reference.type = type;
        ChessPieceType_reference.team = team;
        ChessPieceType_reference.GetComponent<MeshRenderer>().material = teamMaterials[team];
        
        return ChessPieceType_reference;
    }
    //positioning all the chess pieces on tile
    private void PositionAll_ChessPieces()
    {
        for (int x=0; x<Tile_Count_X; x++)
        {
            for (int y=0; y<Tile_Count_Y; y++)
            {
                if(Active_chessPieces[x,y] != null)
                {
                    PositionSingle_ChessPieces(x, y, true);//from (private void PositionSingle_ChessPieces(int x, int y, bool force=false))
                }
            }
        }
    }
    private void PositionSingle_ChessPieces(int x, int y, bool force=false)//(bool force=false) is for smooth position of all chesspieces, when falsh chesspieces will fast move or snap fast on tile, but when truw chesspieces will slow move or snap slowly on tile
    {
        Active_chessPieces[x, y].currentX = x;
        Active_chessPieces[x, y].currentY = y;
        Active_chessPieces[x, y].SetPosition(GetTileCenter(x, y), true);
    }
    private Vector3 GetTileCenter(int x, int y)
    {
        return new Vector3(x * tileSize, yoffset, y * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2);
    }
    //operation
    private bool MoveTo(ChessPiece ChessPieceType_reference, int x, int y)
    {
        Vector2Int previousChess_Position = new Vector2Int(ChessPieceType_reference.currentX, ChessPieceType_reference.currentY);

        //is there another chesspiece on the target position
        if (Active_chessPieces[x, y] != null)
        {
            ChessPiece OtherChessPiece = Active_chessPieces[x, y];
            
            //if the the chess pieces is of same team, don't move to that position
            if (ChessPieceType_reference.team == OtherChessPiece.team)
            {
                return false;
            }

            //if the the black chesspieces has distroy the white chesspieces, let white chesspieces move to dead space
            if (OtherChessPiece.team == 0)
            {
                DeadWhite.Add(OtherChessPiece);
                OtherChessPiece.SetScale(Vector3.one * Dead_ChessSize);
                OtherChessPiece.SetPosition(
                    new Vector3(8 * tileSize, yoffset, -1 * tileSize) - bounds 
                    + new Vector3(tileSize / 2, 0, tileSize / 2) 
                    + DeadSpacing * DeadWhite.Count * Vector3.forward
                );
            }
            else
            {
                //if the the white chesspieces has distroy the black chesspieces, let black chesspieces move to dead space
                DeadBlack.Add(OtherChessPiece);
                OtherChessPiece.SetScale(Vector3.one * Dead_ChessSize);
                OtherChessPiece.SetPosition(
                    new Vector3(-1 * tileSize, yoffset, 8 * tileSize) - bounds 
                    + new Vector3(tileSize / 2, 0, tileSize / 2) 
                    + DeadSpacing * DeadBlack.Count * Vector3.back
                );
                
            }
        }

        Active_chessPieces[x, y] = ChessPieceType_reference;
        Active_chessPieces[previousChess_Position.x, previousChess_Position.y] = null;

        PositionSingle_ChessPieces(x, y);
        
        return true;
    }
    private Vector2Int LookupTileIndex(GameObject hitInfo)
    {
        for (int x = 0; x < Tile_Count_X; x++)
        {
            for (int y = 0; y < Tile_Count_Y; y++)
            {
                if (tiles[x,y] == hitInfo)
                {
                    return new Vector2Int (x,y);
                }
            }
        }
        return -Vector2Int.one;// this code can be deleted, as this will give result(-1 -1) which will cus the game to crash, but as the tile is always generated no need to worry
    }
    private bool ContainValid_ChessMove(ref List<Vector2Int> ChessPiece_moves, Vector2 ChessPiece_Position)
    {
        for (int i = 0; i < ChessPiece_moves.Count; i++)
        {
            if (ChessPiece_moves[i].x == ChessPiece_Position.x && ChessPiece_moves[i].y == ChessPiece_Position.y)
            {
                return true;
            }
        }
        return false;
    }
}