using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int BOARD_WIDTH = 3;
    public const int BOARD_HEIGHT = 3;

    [SerializeField] private BoardCell _cellPrefab;
    public BoardCell[,] cells = new BoardCell[BOARD_WIDTH, BOARD_HEIGHT];
    public int currentPlayer;
    public List<Sprite> playerSprites;
    private AI _ai;

    private void Start()
    {
        PlaceCells();
        _ai = new AI(this);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        ResetAllCells();
        currentPlayer = 0;
    }

    public void PlaceCells()
    {
        for(int x = 0; x < BOARD_WIDTH; x++)
        {
            for(int y = 0; y < BOARD_HEIGHT; y++)
            {
                //Place all cells centered around 0, 0
                PlaceCell(new Vector2(x - (BOARD_WIDTH / 2), y - (BOARD_HEIGHT / 2)) * _cellPrefab.size, new Vector2Int(x, y));
            }
        }
    }

    private void PlaceCell(Vector2 position, Vector2Int index)
    {
        BoardCell placedCell = Instantiate(_cellPrefab, transform);
        placedCell.transform.position = position;
        placedCell.onPiecePlaced += OnPiecePlaced;

        cells[index.x, index.y] = placedCell;
    }

    private void DestroyAllCells()
    {        
        for(int x = 0; x < cells.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < cells.GetUpperBound(1) + 1; y++)
            {
                if (cells[x, y] == null) continue;

                Destroy(cells[x, y].gameObject);
                cells[x, y] = null;
            }
        }        
    }

    private void ResetAllCells()
    {
        for (int x = 0; x < cells.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < cells.GetUpperBound(1) + 1; y++)
            {
                if (cells[x, y] == null) continue;

                cells[x, y].owner = -1;
                cells[x, y].tile.sprite = null;
            }
        }
    }

    private void switchPlayer()
    {
        currentPlayer++;
        currentPlayer %= 2;

        if (currentPlayer == 1) _ai.PlacePiece();
    }

    private int CheckWin()
    {
        int winner = -1;

        for(int i = 0; i < BOARD_WIDTH; i++)
        {
            if(winner == -1) winner = CheckWinOnRow(i);
            if (winner == -1) winner = CheckWinOnCol(i);
        }

        if (winner == -1) winner = CheckWinOnDiag(true);
        if (winner == -1) winner = CheckWinOnDiag(false);

        return winner;
    }

    private int CheckWinOnRow(int row)
    {
        if (
            cells[0, row].owner == 0
            && cells[1, row].owner == 0
            && cells[2, row].owner == 0
        ) return 0;

        if (
            cells[0, row].owner == 1
            && cells[1, row].owner == 1
            && cells[2, row].owner == 1
        ) return 1;

        return -1;
    }

    private int CheckWinOnCol(int col)
    {
        if (
            cells[col, 0].owner == 0
            && cells[col, 1].owner == 0
            && cells[col, 2].owner == 0
        ) return 0;

        if (
            cells[col, 0].owner == 1
            && cells[col, 1].owner == 1
            && cells[col, 2].owner == 1
        ) return 1;

        return -1;
    }

    private bool checkForDraw()
    {
        for(int x = 0; x < cells.GetUpperBound(0) + 1; x++)
        {
            for(int y = 0; y < cells.GetUpperBound(1) + 1; y++)
            {
                if (cells[x, y].owner == -1) return false;
            }
        }

        return true;
    }

    private int CheckWinOnDiag(bool TopRightToBottomLeft)
    {
        if(TopRightToBottomLeft)
        {
            if (
                cells[0, 0].owner == 0
                && cells[1, 1].owner == 0
                && cells[2, 2].owner == 0
            ) return 0;

            if (
                cells[0, 0].owner == 1
                && cells[1, 1].owner == 1
                && cells[2, 2].owner == 1
            ) return 1;
        }
        else
        {
            if (
                cells[2, 0].owner == 0
                && cells[1, 1].owner == 0
                && cells[0, 2].owner == 0
            ) return 0;

            if (
                cells[2, 0].owner == 1
                && cells[1, 1].owner == 1
                && cells[0, 2].owner == 1
            ) return 1;
        }

        return -1;
    }

    public void OnPiecePlaced(BoardCell cell)
    {
        cell.owner = currentPlayer;
        cell.tile.sprite = playerSprites[currentPlayer];

        int winner = CheckWin();       

        if (winner != -1)
        {
            Debug.Log($"Player {winner + 1} wins");
            Debug.Log("Press 'R' to reset");
        }
        else if(checkForDraw())
        {
            Debug.Log($"Draw!");
            Debug.Log("Press 'R' to reset");
        }

        else
        {
            switchPlayer();
        }
    }
}
