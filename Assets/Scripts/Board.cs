using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int Size = 8;
    public const float CellSpace = 0.5f;

    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform cellsTransform;

    private Cell[,] cells;

    private void Awake()
    {
        cells = new Cell[Size, Size];
        InitGrid();
    }

    void InitGrid()
    {
        for(var r = 0; r < Size; r++)
        {
            for(var c = 0; c < Size; c++)
            {
                var pos = GetCellPosition(r, c);
                cells[r, c] = Instantiate(cellPrefab, pos, Quaternion.identity, cellsTransform);
                cells[r, c].name = "( " + r + "_" + c + " )";
                cells[r, c].Row = r;
                cells[r, c].Col = c;
            }
        }
    }

    private Vector3 GetCellPosition(int row, int col)
    {
        float offsetX = col - (Size * CellSpace) + CellSpace;
        float offsetY = row - (Size * CellSpace) + CellSpace;
        return new Vector3(offsetX, offsetY, 0f);
    }

    public void Hover(Vector2Int pos, int polyominoIndex)
    {

    }
}
