using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleGridEditor : EditorWindow
{
    private ObstacleDataSO obstacleData;
    private bool[,] grid;

    [MenuItem("Tools/Obstacle Grid Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleGridEditor>("Obstacle Grid Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Obstacle Grid Settings", EditorStyles.boldLabel);

        obstacleData =
            (ObstacleDataSO)EditorGUILayout.ObjectField("Obstacle Data", obstacleData, typeof(ObstacleDataSO), false);

        if (obstacleData != null)
        {
            if (grid == null || grid.GetLength(0) != obstacleData.rows || grid.GetLength(1) != obstacleData.columns)
            {
                InitGrid();
            }

            DrawGrid();

            if (GUILayout.Button("Save"))
            {
                SaveObstacleData();
            }
        }
    }

    private void InitGrid()
    {
        //For initializing grid based on SO rows and columsn
        grid = new bool[obstacleData.rows, obstacleData.columns];
        foreach (Vector2Int postion in obstacleData.obstaclePositions)
        {
            grid[postion.x, postion.y] = true;
        }
    }

    private void DrawGrid()
    {
        for (int x = 0; x < obstacleData.rows; x++)
        {
            //For checkbox like boxes
            EditorGUILayout.BeginHorizontal();
            for (int y = 0; y < obstacleData.columns; y++)
            {
                grid[x, y] = EditorGUILayout.Toggle(grid[x, y]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void SaveObstacleData()
    {
        obstacleData.obstaclePositions.Clear();

        for (int x = 0; x < obstacleData.columns; x++)
        {
            for (int y = 0; y < obstacleData.rows; y++)
            {
                if (grid[x, y])
                {
                    obstacleData.obstaclePositions.Add(new Vector2Int(x, y));
                }
            }
        }
        
        EditorUtility.SetDirty(obstacleData);
    }
}
