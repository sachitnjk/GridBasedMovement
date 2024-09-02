using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("Panel references")]
    [SerializeField] private RectTransform tileInfoPanel;

    [Header("Other Sub refernces")]
    [SerializeField] private TextMeshProUGUI rowTextField;
    [SerializeField] private TextMeshProUGUI columnTextField;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            //If needed later
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleOnTileHover(int row, int column)
    {
        if (rowTextField != null && columnTextField != null)
        {
            rowTextField.text = row.ToString();
            columnTextField.text = column.ToString();
        }
    }

    public void HandleOnTileHoverEnd()
    {
        rowTextField.text = "Null";
        columnTextField.text = "Null";
    }
}
