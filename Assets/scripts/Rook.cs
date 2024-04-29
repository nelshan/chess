using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override List<Vector2Int> GetAvaiable_ChessMove(ref ChessPiece[,] board, int TileCountX, int TileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        
        //rook can move up or down vertically, also move left or right horizontally on any tile
        //down move
        for (int i = currentY - 1; i >= 0; i--)
        {
            if (board[currentX, i ] == null)
            {
                r.Add(new Vector2Int(currentX, i));
            }

            if (board[currentX, i ] != null)
            {
                if (board[currentX, i ].team != team)
                {
                    r.Add(new Vector2Int(currentX, i));
                }

                break;//this break is for "for loop", when loop enter this "if loop" the for loop will break
            }
        }
        //up move
        for (int i = currentY + 1; i < TileCountY; i++)
        {
            if (board[currentX, i ] == null)
            {
                r.Add(new Vector2Int(currentX, i));
            }

            if (board[currentX, i ] != null)
            {
                if (board[currentX, i ].team != team)
                {
                    r.Add(new Vector2Int(currentX, i));
                }

                break;//this break is for "for loop", when loop enter this "if loop" the for loop will break
            }
        }
        //left move
        for (int i = currentX - 1; i >= 0; i--)
        {
            if (board[i, currentY] == null)
            {
                r.Add(new Vector2Int(i, currentY));
            }

            if (board[i, currentY] != null)
            {
                if (board[i, currentY].team != team)
                {
                    r.Add(new Vector2Int(i, currentY));
                }

                break;//this break is for "for loop", when loop enter this "if loop" the for loop will break
            }
        }
        //right move
        for (int i = currentX + 1; i < TileCountX; i++)
        {
            if (board[i, currentY] == null)
            {
                r.Add(new Vector2Int(i, currentY));
            }

            if (board[i, currentY] != null)
            {
                if (board[i, currentY].team != team)
                {
                    r.Add(new Vector2Int(i, currentY));
                }

                break;//this break is for "for loop", when loop enter this "if loop" the for loop will break
            }
        }

        return r;
    }
}
