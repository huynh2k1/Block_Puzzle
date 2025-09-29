using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Polyominos
{
    private static readonly int[][,] polyominos = new int[][,]
    {
        new int[,]
        {
            { 0, 0, 1},
            { 0, 0, 1},
            { 1, 1, 1}
        }
    };

    public static int[,] Get(int index) => polyominos[index];

    public static int Length => polyominos.Length;

    private static void ReverseRows(int[,] arr)
    {

    }
}
