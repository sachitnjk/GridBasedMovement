using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenCube : MonoBehaviour
{
    [Header("References needed")]
    [SerializeField] private GameObject hoverModel;

    private int row;
    private int column;

    public void SetRowAndColumn(int row, int column)
    {
        this.row = row;
        this.column = column;
    }
    
    #region Hover events
    //using mouse enter & exit unity events
    private void OnMouseEnter()
    {
        hoverModel.SetActive(true);
        UIManager.Instance.HandleOnTileHover(row, column);
    }

    private void OnMouseExit()
    {
        hoverModel.SetActive(false);
        UIManager.Instance.HandleOnTileHoverEnd();
    }
    #endregion
    
}
