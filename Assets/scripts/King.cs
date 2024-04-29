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
}
