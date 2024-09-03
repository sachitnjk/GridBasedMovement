using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Grid/ObstacleData")]
public class ObstacleDataSO : ScriptableObject
{
    public int rows;
    public int columns;
    public List<Vector2Int> obstaclePositions = new List<Vector2Int>();
}