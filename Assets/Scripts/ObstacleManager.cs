using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObstacleDataSO obstacleData;
    [SerializeField] private GameObject obstaclePrefab;
    
    [Header("Uncheck if random rotation on obstacle needs to be turned off")]
    [SerializeField] private bool bObstacleRandomRotation = true;

    private void Start()
    {
        GeneratObstacles();
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
            if (Physics.Raycast(spawnPostion + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity))
            {
                GenCube genCube = hit.collider.GetComponent<GenCube>();
                if (genCube != null)
                {
                    genCube.SetObjectOrEntityOnCube(obstacle);
                }
            }
        }
    }

    private Quaternion GetRandomRotation()
    {
        float randomYRotation = Random.Range(0f, 360f);
        Quaternion rotation = Quaternion.Euler(0, randomYRotation, 0);
        return rotation;
    }
}
