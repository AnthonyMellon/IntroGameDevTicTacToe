using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    public Board board;

    public AI(Board board)
    {
        this.board = board;
    }

    public void PlacePiece()
    {
        for(int x = 0; x < board.cells.GetUpperBound(0) + 1; x++)
        {
            for(int y = 0; y < board.cells.GetUpperBound(1) + 1; y++)
            {
                if (board.cells[x, y].owner == -1)
                {
                    board.cells[x, y].PlacePiece();
                    return;
                }
            }
        }
    }
}
