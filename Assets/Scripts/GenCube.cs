using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenCube : MonoBehaviour
{
    [Header("References needed")]
    [SerializeField] private GameObject hoverModel;
    [SerializeField] private Transform objectPoint;

    private GameObject objectOrEntityOnCube;

    private int row;
    private int column;

    public int gCost;
    public int hCost;
    public GenCube parent;

    public int FCost => gCost + hCost;

    private void Start()
    {
        hoverModel?.SetActive(false);
    }
    
    public void SetObjectOrEntityOnCube(GameObject objectOrEntity)
    {
        objectOrEntityOnCube = objectOrEntity;
    }

    public void RemoveObjectOrEntityOnCube()
    {
        if (objectOrEntityOnCube != null)
        {
            objectOrEntityOnCube = null;
        }
    }

    public Transform GetObjectPoint()
    {
        return objectPoint;
    }
    
    public bool BGetOccupiedStatus()
    {
        return objectOrEntityOnCube != null;
    }

    public int GetRow()
    {
        return row;
    }

    public int GetColumn()
    {
        return column;
    }

    public void SetRowAndColumn(int row, int column)
    {
        this.row = row;
        this.column = column;
    }
    
    #region Hover events
    //using mouse enter & exit unity events
    private void OnMouseEnter()
    {
        if (objectOrEntityOnCube == null)
        {
            hoverModel.SetActive(true);
        }

        UIManager.Instance?.HandleOnTileHover(row, column);
    }

    private void OnMouseExit()
    {
        hoverModel?.SetActive(false);

        UIManager.Instance?.HandleOnTileHoverEnd();
    }
    #endregion
    
}
