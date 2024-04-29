using UnityEngine;
using System.Collections.Generic;

public class Queen : ChessPiece
{
    public override List<Vector2Int> GetAvaiable_ChessMove(ref ChessPiece[,] board, int TileCountX, int TileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        
        // queen can  moves like a rook and bishop combined.

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
