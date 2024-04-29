using System;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public override Special_Move GetSpecialMoves(ref ChessPiece[,] board, ref List<Vector2Int[]> ChessMove_list, ref List<Vector2Int> Avaiable_ChessMoves)
    {   
        int direction = (team == 0 ) ? 1 : -1;

        //implementung Special_Move.promotion for pawn
        /*promotion is the replacement of a pawn with a new piece when the pawn is moved to its last rank.
        The player replaces the pawn immediately with a queen, rook, bishop, or knight of the same color.
        The new piece does not have to be a previously captured piece.
        Promotion is mandatory when moving to the last rank; the pawn cannot remain as a pawn.*/
        if ((team == 0 && currentY == 6) || (team == 1 && currentY == 1))//if team is of whiteteam's pawn is at tile 6 or if team is of black team's pawn is at tile 1
        {
            return Special_Move.Promotion;
        }

        //en passan move for pawn
        if (ChessMove_list.Count > 0)
        {
            Vector2Int[] lastMoved_chess = ChessMove_list[ChessMove_list.Count - 1];

            //if the last piece moved was pawn
            if(board[lastMoved_chess[1].x, lastMoved_chess[1].y].type == ChessPieceType.Pawn)//get the second part of move that is 0,1,2,....
            {
                if (Mathf.Abs(lastMoved_chess[0].y - lastMoved_chess[1].y) == 2)//if the last piece moved "2 time already(2)" in either direction
                {
                    if (board[lastMoved_chess[1].x, lastMoved_chess[1].y].team != team)//if move was from other team
                    {
                        if (lastMoved_chess[1].y == currentY)//if bothe pawns are on same y 
                        {
                            if (lastMoved_chess[1].x == currentX - 1)//pawn is laned on left side
                            {
                                Avaiable_ChessMoves.Add(new Vector2Int(currentX - 1, currentY + direction));
                                return Special_Move.EnPassant;
                            }

                            if (lastMoved_chess[1].x == currentX + 1)//pawn is laned on right side
                            {
                                Avaiable_ChessMoves.Add(new Vector2Int(currentX + 1, currentY + direction));
                                return Special_Move.EnPassant;
                            }
                        }
                    }
                }
            }
        }

        return Special_Move.None;
    }

}
