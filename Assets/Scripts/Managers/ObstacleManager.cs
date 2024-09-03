using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObstacleDataSO obstacleData;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private PlayerSpawner spawner;
    [SerializeField] private EnemySpawner enemySpawner;
    
    [Header("Uncheck if random rotation on obstacle needs to be turned off")]
    [SerializeField] private bool bObstacleRandomRotation = true;

    private void Start()
    {
        EventManager.Instance.OnGridGenerated += GeneratObstacles;
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnGridGenerated -= GeneratObstacles;
    }

    private void GeneratObstacles()
    {
        foreach (Vector2Int position in obstacleData.obstaclePositions)
        {
            Vector3 spawnPostion = new Vector3(position.x, 0, position.y);
            GameObject obstacle;
            //Added for randomizing obstacle rotation
            if (bObstacleRandomRotation)
            { 
                obstacle = Instantiate(obstaclePrefab, spawnPostion, GetRandomRotation());
            }
            else
            {
                obstacle = Instantiate(obstaclePrefab, spawnPostion, Quaternion.identity);
            }
            
            //Functionality for setting obstacle variable on the cube script
            RaycastHit hit;
            if (Physics.Raycast(spawnPostion + Vector3.up * 2, Vector3.down, out hit, Mathf.Infinity, (1<<6)))
            {
                GenCube genCube = hit.collider.GetComponent<GenCube>();
                if (genCube != null)
                {
                    genCube.SetObjectOrEntityOnCube(obstacle);
                }
            }
        }
        
        spawner.SpawnPlayer();
        enemySpawner.SpawnEnemy();
    }

    private Quaternion GetRandomRotation()
    {
        float randomYRotation = Random.Range(0f, 360f);
        Quaternion rotation = Quaternion.Euler(0, randomYRotation, 0);
        return rotation;
    }
}
