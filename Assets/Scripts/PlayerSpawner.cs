using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PathFinding pathFinding;
    [SerializeField] private GameObject player;

    private GenCube[,] gridArray;
    private GenCube cubeToSpawnAt;

    private void Start()
    {
        if (gridManager != null)
        {
            gridArray = gridManager.GetGridArray();
        }
    }

    public void SpawnPlayer()
    {
        if (gridArray == null && gridManager != null)
        {
            gridArray = gridManager.GetGridArray();
        }

        foreach (GenCube cube in gridArray)
        {
            if (!cube.BGetOccupiedStatus())
            {
                GameObject instantiatedPlayer = Instantiate(player, cube.GetObjectPoint().position, Quaternion.identity);
                cube.SetObjectOrEntityOnCube(player);
                
                PlayerMove playerMove = instantiatedPlayer?.GetComponent<PlayerMove>();
                playerMove.InitPlayer(pathFinding, cube);
                
                break;
            }
        }
    }
}
