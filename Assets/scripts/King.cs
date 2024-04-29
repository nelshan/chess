using UnityEngine;
using System.Collections.Generic;

public class King : ChessPiece
{
    public override List<Vector2Int> GetAvaiable_ChessMove(ref ChessPiece[,] board, int TileCountX, int TileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //king can move one square in any direction(top,right,down,left).
        //right part of king move
        if  (currentX + 1 < TileCountX)
        {   
            // top right
            if(currentY + 1 < TileCountY)
            {
                if (board[currentX + 1, currentY + 1] == null)//if the tile is empty add to list
                {
                    r.Add(new Vector2Int(currentX + 1, currentY + 1));
                }
                else if (board[currentX + 1, currentY + 1].team != team)
                {   
                    r.Add(new Vector2Int(currentX + 1, currentY + 1));
                }
            }
            
            
            //middle right
            if (board[currentX + 1, currentY] == null)//if the tile is empty add to list
            {
                r.Add(new Vector2Int(currentX + 1, currentY));
            }
            else if (board[currentX + 1, currentY].team != team)
            {
                r.Add(new Vector2Int(currentX + 1, currentY));
            }
            

            // bottom right
            if(currentY - 1 >= 0)
            {
                if (board[currentX + 1, currentY - 1] == null)//if the tile is empty add to list
                {
                    r.Add(new Vector2Int(currentX + 1, currentY - 1));
                }
                else if (board[currentX + 1, currentY - 1].team != team)
                {   
                    r.Add(new Vector2Int(currentX + 1, currentY - 1));
                }
            }
        }

        //left part of king move
        if  (currentX - 1 >= 0)
        {   
            // top left
            if(currentY + 1 < TileCountY)
            {
                if (board[currentX - 1, currentY + 1] == null)//if the tile is empty add to list
                {
                    r.Add(new Vector2Int(currentX - 1, currentY + 1));
                }
                else if (board[currentX - 1, currentY + 1].team != team)
                {   
                    r.Add(new Vector2Int(currentX - 1, currentY + 1));
                }
            }
            
            
            //middle left
            if (board[currentX - 1, currentY] == null)//if the tile is empty add to list
            {
                r.Add(new Vector2Int(currentX - 1, currentY));
            }
            else if (board[currentX - 1, currentY].team != team)
            {
                r.Add(new Vector2Int(currentX - 1, currentY));
            }
            

            // bottom left
            if(currentY - 1 >= 0)
            {
                if (board[currentX - 1, currentY - 1] == null)//if the tile is empty add to list
                {
                    r.Add(new Vector2Int(currentX - 1, currentY - 1));
                }
                else if (board[currentX - 1, currentY - 1].team != team)
                {   
                    r.Add(new Vector2Int(currentX - 1, currentY - 1));
                }
            }
        }

        //top middel part of king move, as right and left part is already set
        if (currentY + 1 < TileCountY)
        {
            if (board[currentX, currentY + 1] == null || board[currentX, currentY + 1].team != team)
            {
                r.Add(new Vector2Int(currentX, currentY +1));
            }
        }

        //down middel part of king move, as right and left part is already set
        if (currentY - 1 >= 0)
        {
            if (board[currentX, currentY - 1] == null || board[currentX, currentY - 1].team != team)
            {
                r.Add(new Vector2Int(currentX, currentY - 1));
            }
        }

        return r;
    }

    //Castling move for king and rook
    public override Special_Move GetSpecialMoves(ref ChessPiece[,] board, ref List<Vector2Int[]> ChessMove_list, ref List<Vector2Int> Avaiable_ChessMoves)
    {
        Special_Move r = Special_Move.None;

        //looling for the starting position for king, left and right rook that is in both team white and black
        var KingMove = ChessMove_list.Find(m => m[0].x == 4 && m[0].y == ((team == 0) ? 0 : 7));
        var LeftRook = ChessMove_list.Find(m => m[0].x == 0 && m[0].y == ((team == 0) ? 0 : 7));
        var RightRook = ChessMove_list.Find(m => m[0].x == 7 && m[0].y == ((team == 0) ? 0 : 7));

        //if the king hasnt moved and still at its own tile
        if (KingMove == null && currentX == 4)
        {
            //white team
            if (team == 0)
            {
                //checking left rook
                //if left rook hasnt moved at all
                if (LeftRook == null)
                {
                    //and is still at its own tile
                    if (board[0, 0].type == ChessPieceType.Rook)
                    {
                        //if rook is of white team
                        if (board[0, 0].team == 0)
                        {
                            if (board[3, 0] == null) //if there no chesspiece to the left of king piece upto 3 left tile
                            {
                                if (board[2, 0] == null)//if there no chesspiece to the left of king piece upto 2 left tile
                                {
                                    if (board[1, 0] == null)//if there no chesspiece to the left of king piece upto 1 left tile
                                    {
                                        Avaiable_ChessMoves.Add(new Vector2Int(2, 0));//there "(2, 0)" cus the king piece will go 4 tile spce of "x-axis".i.e: (4-2=2).so, 2 move left
                                        
                                        r = Special_Move.Castling;
                                    }
                                }
                            }
                        }
                    }
                }
                
                //checking right rook
                if (RightRook == null)//if left rook hasnt moved at all
                {
                    //and is still at its own tile
                    if (board[7, 0].type == ChessPieceType.Rook)
                    {
                        //if rook is of white team
                        if (board[7, 0].team == 0)
                        {
                            if (board[5, 0] == null) //if there no chesspiece to the left of king piece upto 5 left tile
                            {
                                if (board[6, 0] == null)//if there no chesspiece to the left of king piece upto 6 left tile
                                {
                                    Avaiable_ChessMoves.Add(new Vector2Int(6, 0));//there "(2, 0)" cus the king piece will go 4 tile spce of "x-axis".i.e: (6-4=2).so, 2 move right
                                        
                                    r = Special_Move.Castling;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //black team
                //checking left rook               
                if (LeftRook == null)//if left rook hasnt moved at all
                {
                    //and is still at its own tile
                    if (board[0, 7].type == ChessPieceType.Rook)
                    {                        
                        if (board[0, 7].team == 1)//if rook is of back team
                        {
                            if (board[3, 7] == null) //if there no chesspiece to the left of king piece upto 3 left tile between king and rook
                            {
                                if (board[2, 7] == null)//if there no chesspiece to the left of king and right of rook piece
                                {
                                    if (board[1, 7] == null)//if there no chesspiece to the left of king and right of rook piece
                                    {
                                        Avaiable_ChessMoves.Add(new Vector2Int(2, 7));
                                        
                                        r = Special_Move.Castling;
                                    }
                                }
                            }
                        }
                    }
                }
                
                //checking right rook               
                if (RightRook == null)//if left rook hasnt moved at all
                {
                    //and is still at its own tile
                    if (board[7, 7].type == ChessPieceType.Rook)
                    {                       
                        if (board[7, 7].team == 1)//if rook is of back team
                        {
                            if (board[5, 7] == null) //if there no chesspiece to the left of king and right of rook piece
                            {
                                if (board[6, 7] == null)//if there no chesspiece to the left of king and right of rook piece
                                {
                                    Avaiable_ChessMoves.Add(new Vector2Int(6, 7));
                                        
                                    r = Special_Move.Castling;
                                }
                            }
                        }
                    }
                }
            }
        }

        return r;
    }
}
