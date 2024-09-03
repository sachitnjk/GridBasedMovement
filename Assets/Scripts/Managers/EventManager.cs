using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Action OnGridGenerated;
    
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

    public void InvokeOnGridGenerated()
    {
        OnGridGenerated?.Invoke();
    }
}
