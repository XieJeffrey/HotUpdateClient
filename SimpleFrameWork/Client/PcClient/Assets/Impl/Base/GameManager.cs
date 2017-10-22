using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager:MonoBehaviour
{
     void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("GameManager awake");
    }

    void Update()
    {
        
    }


}

