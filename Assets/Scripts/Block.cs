using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public const int Size = 4;

    [SerializeField] Cell _cellPrefab;
    private readonly Cell[,] cells = new Cell[Size, Size];

    public void Initialize()
    {
        for(var r = 0; r < Size; ++r)
        {
            for(var c = 0; c < Size; ++c)
            {
                cells[r, c] = Instantiate(_cellPrefab, transform);
                cells[r, c].transform.localPosition = new Vector2(c - Size/2 + 1, r - Size/2 + 1);
            }
        }
    }

    //Hiển thị hình dạng polyomino : 1 -> active, 0 -> deactive
    public void Show(int polyominoIndex)
    {
        var polyomino = Polyominos.Get(polyominoIndex);
        var polyominoRows = polyomino.GetLength(0);
        var polyominoColumns = polyomino.GetLength(1);
        Hide();
        for(var r = polyominoRows - 1; r >= 0; --r)
        {
            for (var c = polyominoColumns - 1; c >= 0; --c)
            {
                if (polyomino[r, c] == 1)
                {
                    cells[r, c].Normal();
                }
            }
        }
    }

    void Hide()
    {
        foreach(var cell in cells)
        {
            cell.Hide();
        }
    }
}
