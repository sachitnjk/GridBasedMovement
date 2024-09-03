using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PathFinding pathFinding;
    [SerializeField] private GameObject enemyPrefab;

    private GenCube[,] gridArray;

    private void Start()
    {
        if (gridManager != null)
        {
            gridArray = gridManager.GetGridArray();
        }
    }

    public void SpawnEnemy()
    {
        if (gridArray == null && gridManager != null)
        {
            gridArray = gridManager.GetGridArray();
        }

        GenCube lastUnoccupiedCube = null;

        foreach (GenCube cube in gridArray)
        {
            if (!cube.BGetOccupiedStatus())
            {
                lastUnoccupiedCube = cube;
            }
        }
        
        if (lastUnoccupiedCube != null)
        {
            GameObject instantiatedEnemy = Instantiate(enemyPrefab, lastUnoccupiedCube.GetObjectPoint().position, Quaternion.identity);
            lastUnoccupiedCube.SetObjectOrEntityOnCube(enemyPrefab);

            EnemyBase enemyBase = instantiatedEnemy.GetComponent<EnemyBase>();
            enemyBase.InitAI(pathFinding, lastUnoccupiedCube);
        }
    }
}
