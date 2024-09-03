using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour, IAI
{
    [Header("enemy Refernences")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject enemySpriteObject;
    [SerializeField] private Animator enemyAnimator;

    [Range(2f, 15f)] [SerializeField] private float moveSpeed = 3f;

    private PathFinding pathFinding;
    private GenCube currentGenCube;
    private GenCube targetGenCube;
    private List<GenCube> path;
    private int pathIndex;
    private bool isMoving = false;
    private Vector3 spriteLookAtDirection;

    private void Start()
    {
        EventManager.Instance.OnPlayerMove += HandleOnPlayerMove;
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnPlayerMove -= HandleOnPlayerMove;
    }

    private void Update()
    {
        UpdateAI();
    }

    public void UpdateAI()
    {
        if (targetGenCube != null && !isMoving)
        {
            path = pathFinding.FindPath(currentGenCube, targetGenCube);
            if (path != null)
            {
                currentGenCube.RemoveObjectOrEntityOnCube();
                pathIndex = 0;
                isMoving = true;
                enemyAnimator.SetBool("isMoving", true);
            }
        }

        if (path != null && pathIndex < path.Count)
        {
            MoveAlongPath();
            SpriteFaceMaintain();
        }
    }
    
    private void MoveAlongPath()
    {
        if (Vector3.Distance(transform.position, path[pathIndex].transform.position) < 0.1f)
        {
            pathIndex++;
        }

        if (pathIndex < path.Count)
        {
            Vector3 nextPosition = path[pathIndex].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            targetGenCube.SetObjectOrEntityOnCube(this.gameObject);
            currentGenCube = path[pathIndex - 1];
            path = null;
            isMoving = false;
            targetGenCube = null;
            enemyAnimator.SetBool("isMoving", false);
        }
    }
    
    private void SpriteFaceMaintain()
    {
        if (path != null && pathIndex < path.Count)
        {
            Vector3 direction = path[pathIndex].transform.position - transform.position;
            direction.y = 0f;
            spriteLookAtDirection = direction.normalized;
            
            Quaternion targetRotation = Quaternion.LookRotation(spriteLookAtDirection);
            enemySpriteObject.transform.rotation = targetRotation;
            
            if(!spriteRenderer.flipX && direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (spriteRenderer.flipX && direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    public void InitAI(PathFinding pathFinding, GenCube startCube)
    {
        this.pathFinding = pathFinding;
        this.currentGenCube = startCube;
    }
    
    public void HandleOnPlayerMove(GenCube playerTargetCube)
    {
        List<GenCube> neighbourCubes = pathFinding.GetNeighbours(playerTargetCube);
        for (int i = neighbourCubes.Count - 1; i >= 0; i--)
        {
            if (neighbourCubes[i].BGetOccupiedStatus())
            {
                neighbourCubes.RemoveAt(i);
            }
        }

        if (neighbourCubes.Count != 0)
        {
            targetGenCube = neighbourCubes[Random.Range(0, neighbourCubes.Count)];
        }
    }
}
