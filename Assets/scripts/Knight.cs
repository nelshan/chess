using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override List<Vector2Int> GetAvaiable_ChessMove(ref ChessPiece[,] board, int TileCountX, int TileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //either moves up or down one square vertically and over two squares horizontally 
        //OR up or down two squares vertically and over one square horizontally
        //top move
        int x = currentX + 1;
        int y = currentY + 2;

        if (x < TileCountX && y < TileCountY)
        {
            if (board[x, y] == null || board[x, y].team != team)
            {
                r.Add(new Vector2Int(x, y));
            }
        }
        //
        x = currentX + 2;
        y = currentY + 1;

        if (x < TileCountX && y < TileCountY)
        {
            if (board[x, y] == null || board[x, y].team != team)
            {
                r.Add(new Vector2Int(x, y));
            }
        }

        //top left
        x = currentX - 1;
        y = currentY + 2;

        if (x >= 0 && y < TileCountY)
        {
            if (board[x, y] == null || board[x, y].team != team)
            {
                r.Add(new Vector2Int(x, y));
            }
        }
        x = currentX - 2;
        y = currentY + 1;
        if (x >= 0 && y < TileCountY)
        {
            if (board[x, y] == null || board[x, y].team != team)
            {
                r.Add(new Vector2Int(x, y));
            }
        }
        //buttom right
        x = currentX + 1;
        y = currentY - 2;
        if (x < TileCountX && y >= 0)
        {
            if (board[x, y] == null || board[x, y].team != team)
            {
                r.Add(new Vector2Int(x, y));
            }
        }
        x = currentX + 2;
        y = currentY - 1;
        if (x < TileCountX && y >= 0)
        {
            if (board[x, y] == null || board[x, y].team != team)
            {
                r.Add(new Vector2Int(x, y));
            }
        }
        //buttom left
        x = currentX - 1;
        y = currentY - 2;
        if (x >= 0 && y >= 0)
        {
            if (board[x, y] == null || board[x, y].team != team)
            {
                r.Add(new Vector2Int(x, y));
            }
        }
        x = currentX - 2;
        y = currentY - 1;
        if (x >= 0 && y >= 0)
        {
            if (board[x, y] == null || board[x, y].team != team)
            {
                r.Add(new Vector2Int(x, y));
            }
        }


        return r;
    }
}
