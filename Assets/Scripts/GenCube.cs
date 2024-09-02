using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenCube : MonoBehaviour
{
    [Header("References needed")]
    [SerializeField] private GameObject hoverModel;

    #region Hover events
    //using mouse enter & exit unity events
    private void OnMouseEnter()
    {
        hoverModel.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverModel.SetActive(false);
    }
    #endregion
    
}
