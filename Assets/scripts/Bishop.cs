using UnityEngine;
using System.Collections.Generic;

public class Bishop : ChessPiece
{
    public override List<Vector2Int> GetAvaiable_ChessMove(ref ChessPiece[,] board, int TileCountX, int TileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        // Bishop can moves only on diagonals forward and backward.
        //top right
        for (int x = currentX + 1, y = currentY + 1; x < TileCountX && y < TileCountY; x++, y++)
        {
            //if the tile that bishop can go is empty, add to the avauable chessmove
            if (board[x, y] == null)
            {
                r.Add(new Vector2Int(x, y));
            }
            else
            {
                //if the chess piece that is on the way of bishop avauable chessmove and chesspiece is not of same team
                //add to the avauable chessmove
                if (board[x, y].team != team)
                {
                    r.Add(new Vector2Int(x, y));
                }
                break;
            }
        }
        //top left
        for (int x = currentX - 1, y = currentY + 1; x >= 0 && y < TileCountY; x--, y++)
        {
            //if the tile that bishop can go is empty, add to the avauable chessmove
            if (board[x, y] == null)
            {
                r.Add(new Vector2Int(x, y));
            }
            else
            {
                //if the chess piece that is on the way of bishop avauable chessmove and chesspiece is not of same team
                //add to the avauable chessmove
                if (board[x, y].team != team)
                {
                    r.Add(new Vector2Int(x, y));
                }
                break;
            }
        }
        //bottom right
        for (int x = currentX + 1, y = currentY - 1; x < TileCountX && y >= 0; x++, y--)
        {
            //if the tile that bishop can go is empty, add to the avauable chessmove
            if (board[x, y] == null)
            {
                r.Add(new Vector2Int(x, y));
            }
            else
            {
                //if the chess piece that is on the way of bishop avauable chessmove and chesspiece is not of same team
                //add to the avauable chessmove
                if (board[x, y].team != team)
                {
                    r.Add(new Vector2Int(x, y));
                }
                break;
            }
        }
        //bottom left
        for (int x = currentX - 1, y = currentY - 1; x >= 0 && y >= 0; x--, y--)
        {
            //if the tile that bishop can go is empty, add to the avauable chessmove
            if (board[x, y] == null)
            {
                r.Add(new Vector2Int(x, y));
            }
            else
            {
                //if the chess piece that is on the way of bishop avauable chessmove and chesspiece is not of same team
                //add to the avauable chessmove
                if (board[x, y].team != team)
                {
                    r.Add(new Vector2Int(x, y));
                }
                break;
            }
        }



        return r;
    }
}
