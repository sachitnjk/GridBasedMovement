using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player References")]
    // [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator playerAnimator;

    private bool isMoving;

    private void Update()
    {
        if (!isMoving)
        {
            //Register Input and do action
        }
    }
}
