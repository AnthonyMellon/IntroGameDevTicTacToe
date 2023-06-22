using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    const int BOARD_WIDTH = 3;
    const int BOARD_HEIGHT = 3;

    [SerializeField] private BoardCell _cellPrefab;
    BoardCell[,] cells = new BoardCell[BOARD_WIDTH, BOARD_HEIGHT];
    public int currentPlayer;
    public List<Sprite> playerSprites;

    private void Start()
    {
        PlaceCells();
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

    public void OnPiecePlaced(BoardCell cell)
    {
        cell.owner = currentPlayer;
        cell.tile.sprite = playerSprites[currentPlayer];

        switchPlayer();
    }

    private void switchPlayer()
    {
        currentPlayer++;
        currentPlayer %= 2;
    }
}
