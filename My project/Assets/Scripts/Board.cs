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

    BoardCell[,] pieces = new BoardCell[BOARD_WIDTH, BOARD_HEIGHT];

    private void Start()
    {
        PlaceCells();
    }

    private void Update()
    {
       
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

        pieces[index.x, index.y] = placedCell;
    }

    private void DestroyAllCells()
    {        
        for(int x = 0; x < pieces.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < pieces.GetUpperBound(1) + 1; y++)
            {
                if (pieces[x, y] == null) continue;

                Destroy(pieces[x, y].gameObject);
                pieces[x, y] = null;
            }
        }        
    }

    private void ResetAllCells()
    {

    }
}
