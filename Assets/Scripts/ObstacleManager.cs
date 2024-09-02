using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObstacleDataSO obstacleData;
    [SerializeField] private GameObject obstaclePrefab;

    private void Start()
    {
        GeneratedObstacle();
    }

    private void GeneratedObstacle()
    {
        foreach (Vector2Int position in obstacleData.obstaclePositions)
        {
            Vector3 spawnPostion = new Vector3(position.x, 0, position.y);
            Instantiate(obstaclePrefab, spawnPostion, Quaternion.identity);
        }
    }
}
