using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public const int Size = 4;

    [SerializeField] Board board;
    [SerializeField] Cell _cellPrefab;
    private readonly Cell[,] cells = new Cell[Size, Size];
    private readonly Vector3 inputOffset = new(0f, 2f, 0f);

    int _polyominoIndex;

    Vector3 _prevMousePos;
    Vector3 _curMousePos;
    Vector3 _initPos;
    Vector3 _initScale;
    Vector3 _inputDelta;
    Vector2 center;

    Vector2Int previousDragPoint;
    Vector2Int currentDragPoint;

    //Cache
    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Initialize()
    {
        _initPos = transform.position;
        _initScale = transform.localScale;

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
        _polyominoIndex = polyominoIndex;

        Hide();

        var polyomino = Polyominos.Get(polyominoIndex);
        var polyominoRows = polyomino.GetLength(0);
        var polyominoColumns = polyomino.GetLength(1);

        center = new Vector2(polyominoColumns * 0.5f, polyominoRows * 0.5f);
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

    private void OnMouseDown()
    {
        _prevMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _prevMousePos.z = 0;
        _inputDelta = transform.position - _prevMousePos + inputOffset;

        transform.localPosition = _initPos + _inputDelta;
        transform.localScale = Vector3.one;

        currentDragPoint = Vector2Int.RoundToInt((Vector2)transform.position - center);
        previousDragPoint = currentDragPoint;
    }

    private void OnMouseDrag()
    {
        _curMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        if (_curMousePos != _prevMousePos)
        {
            _curMousePos.z = 0;
            _prevMousePos = _curMousePos;

            transform.localPosition = _curMousePos + _inputDelta;

            currentDragPoint = Vector2Int.RoundToInt((Vector2)transform.position - center);
            if(currentDragPoint != previousDragPoint)
            {
                previousDragPoint = currentDragPoint;
                Debug.Log($"Drag Point {currentDragPoint}");
            }
        }

    }
    private void OnMouseUp()
    {
        BackToInitPos();
    }

    void BackToInitPos()
    {
        _prevMousePos = Vector2.positiveInfinity;
        transform.position = _initPos;
        transform.localScale = _initScale;
    }
}
