using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    private void Start()
    {
        //Độ rộng của Board / số block (3)
        var blockWidth = (float)Board.Size / blocks.Length;
        var cellSize = (float)Board.Size / (Block.Size * blocks.Length + blocks.Length + 1);

        for(var i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.localPosition = new(blockWidth * (i + 0.5f) - (float)Board.Size / 2, cellSize * 4.0f - Board.Size, 0.0f);
            blocks[i].transform.localScale = Vector2.one * cellSize;
            blocks[i].Initialize();
        }

        Generate();
    }

    private void Generate()
    {
        for(var i = 0; i < blocks.Length; i++)
        {
            blocks[i].Show(0);
        }
    }
}
