using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Action OnGridGenerated;
    public Action<GenCube> OnPlayerMove;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            //If needed
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InvokeOnPlayerMove(GenCube targetCube)
    {
        OnPlayerMove?.Invoke(targetCube);
    }
    
    public void InvokeOnGridGenerated()
    {
        OnGridGenerated?.Invoke();
    }
}
