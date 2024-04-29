using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public override List<Vector2Int> GetAvaiable_ChessMove(ref ChessPiece[,] board, int TileCountX, int TileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        
        // "0" is white team, "1" is black team
        int ChessPiece_direction = (team == 0) ? 1 : -1;//if team is white go up, if black team go down

        //A pawn moves forward "one square", unless it is that pawn's first move
        if (board[currentX, currentY + ChessPiece_direction] == null)
        {
            r.Add(new Vector2Int(currentX, currentY + ChessPiece_direction));
        }
        
        //If it is the pawn's first move, then it can move "one or two squares".if a pawn has already been moved, it can never move two squares again.
        //pawn can only move forward if it is not blocked by another piece
        if (board[currentX, currentY + ChessPiece_direction] == null)
        {
            //this pawn's move is for white team
            if (team == 0 && currentY == 1 && board[currentX, currentY + (ChessPiece_direction * 2)] == null)
            {
                r.Add(new Vector2Int(currentX, currentY + (ChessPiece_direction * 2)));
            }
            //this pawn's move is for black team
            if (team == 1 && currentY == 6 && board[currentX, currentY + (ChessPiece_direction * 2)] == null)
            {
                r.Add(new Vector2Int(currentX, currentY + (ChessPiece_direction * 2)));
            }
        }
        
        //pawn can attacks or captures one square forward diagonally(En Passant) in each direction
        //this is for pawn "right" square forward diagonally(En Passant) only
        if (currentX != TileCountX - 1)
        {
            if (board[currentX + 1, currentY + ChessPiece_direction] != null && board[currentX + 1, currentY + ChessPiece_direction].team != team)
            {
                r.Add(new Vector2Int(currentX + 1, currentY + ChessPiece_direction));
            }
        }
        //this is for pawn "left" square forward diagonally(En Passant) only
        if (currentX != 0)
        {
            if (board[currentX - 1, currentY + ChessPiece_direction] != null && board[currentX - 1, currentY + ChessPiece_direction].team != team)
            {
                r.Add(new Vector2Int(currentX - 1, currentY + ChessPiece_direction));
            }
        }

        return r;
    }
}
