using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum ChessPieceType
{
    None = 0,
    Pawn = 1,
    Rook = 2,
    Knight = 3,
    Bishop = 4,
    Queen = 5,
    King = 6
}

public class ChessPiece : MonoBehaviour
{
    public int team;
    public int currentX;
    public int currentY;
    public ChessPieceType type;
    public Vector3 desiredPosition;
    public Vector3 desiredScale = Vector3.one;
    private bool gradualScale = false; // Added variable to control gradual scaling


    /*this start is for fixing the chess rotation. neede fix
    private void Start()
    {
        transform.rotation = quaternion.Euler((team == 0) ? new Vector3 (0,0,0) : new Vector3(0,0,0));
    }*/

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);

        if (gradualScale) // Only apply gradual scaling when gradualScale is true
        {
            transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * 10);
        }
    }

    public virtual List<Vector2Int> GetAvaiable_ChessMove(ref ChessPiece[,] board, int TileCounX, int TileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        /*r.Add(new Vector2Int(3, 3));
        r.Add(new Vector2Int(3, 4));
        r.Add(new Vector2Int(4, 3));
        r.Add(new Vector2Int(4, 4));*/

        return r;

    }

    public virtual void SetPosition(Vector3 position, bool force = true)
    {
        desiredPosition = position;
        if (force)
        {
            transform.position = desiredPosition;
        }
    }

    public virtual void SetScale(Vector3 scale, bool force = true, bool gradual = false) // Added 'gradual' parameter
    {
        desiredScale = scale;
        if (force)
        {
            transform.localScale = desiredScale;
        }
        else
        {
            gradualScale = gradual; // Set gradualScale based on 'gradual' parameter
        }
    }

}
