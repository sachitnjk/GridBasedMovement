using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GridManager : MonoBehaviour
{
    [Header("Values and References required")]
    [SerializeField] private int rows = 10;
    [SerializeField] private int columns = 10;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private GameObject genCubePrefab;

    private GameObject[,] gridArray;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        gridArray = new GameObject[rows, columns];

        Vector3 startPos = transform.position;
        
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                Vector3 cellPosition = startPos + new Vector3(x * cellSize, 0, y * cellSize);
                gridArray[x, y] = InstantiateCell(cellPosition);
            }
        }
    }

    private GameObject InstantiateCell(Vector3 cellPos)
    {
        if (genCubePrefab != null)
        {
            GameObject instantiatedGenCube =
                Instantiate(genCubePrefab, cellPos, Quaternion.identity, this.gameObject.transform);
            instantiatedGenCube.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
            
            return instantiatedGenCube;
        }

        Debug.LogError("genCube prefab not set in editor");
        return null;
    }
}
