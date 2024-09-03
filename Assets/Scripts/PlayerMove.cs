using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject playerSpriteObject;
    [SerializeField] private Animator playerAnimator;

    [Range(2f, 15f)]
    [SerializeField] private float moveSpeed = 5f;
    
    private PathFinding pathFinding;

    private PlayerInput playerInput;
    private InputAction leftClickAction;
    private InputAction rightClickAction;

    private Vector3 spriteLookAtDirection;
    
    private GenCube currentGenCube;
    private List<GenCube> path;
    private int pathIndex;
    
    private bool isMoving = false;

    private void Start()
    {
        playerInput = InputHandler.Instance?.GetPlayerInput();
        if (playerInput != null)
        {
            leftClickAction = playerInput.actions["LeftClick"];
            rightClickAction = playerInput.actions["RightClick"];
        }
    }

    private void Update()
    {
        if (!isMoving && leftClickAction.triggered)
        {
            // Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GenCube targetGenCube = hit.collider.GetComponent<GenCube>();
                if (targetGenCube != null && !targetGenCube.BGetOccupiedStatus())
                {
                    path = pathFinding.FindPath(currentGenCube, targetGenCube);
                    pathIndex = 0;
                    isMoving = true;
                    playerAnimator.SetBool("isMoving", true);
                    
                    currentGenCube.RemoveObjectOrEntityOnCube();
                }
                else
                {
                    Debug.Log("No valid log found");
                }
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
            currentGenCube = path[pathIndex - 1];
            path = null;
            isMoving = false;
            playerAnimator.SetBool("isMoving", false);
        }
    }

    public void InitPlayer(PathFinding pathFinding, GenCube genCube)
    {
        this.pathFinding = pathFinding;
        this.currentGenCube = genCube;
    }
    
    private void SpriteFaceMaintain()
    {
        if (path != null && pathIndex < path.Count)
        {
            Vector3 direction = path[pathIndex].transform.position - transform.position;
            direction.y = 0f;
            spriteLookAtDirection = direction.normalized;
            
            Quaternion targetRotation = Quaternion.LookRotation(spriteLookAtDirection);
            playerSpriteObject.transform.rotation = targetRotation;
            
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

}
