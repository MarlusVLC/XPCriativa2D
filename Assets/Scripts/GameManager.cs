﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Audio;
using UnityEngine;

using Aux_Classes;

public class GameManager : MonoBehaviour
{

    private Dictionary<GameObject, RespawnState> _respawnStates = new Dictionary<GameObject, RespawnState>();
    private AudioLib _AudioLib;



    private static GameManager INSTANCE;
    public static GameManager getInstance
    {
        get { return INSTANCE; }
    }

    private void Awake()
    {
        //SINGLETON check
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(this.gameObject);
        }
        //SINGLETON instantiate
        else
        {
            INSTANCE = this;
        }
        
        
        
    }
    
    


    public void SetRespawnState(GameObject gameObject, Vector3 position, Vector3 velocity, float rotation)
    {
        RespawnState respawnState = new RespawnState(position, velocity, rotation);


        if (_respawnStates.ContainsKey(gameObject))
        {
            _respawnStates[gameObject] = respawnState;
        }
        else
        {
            _respawnStates.Add(gameObject, respawnState);
        }
    }

    public RespawnState GetRespawnState(GameObject gameObject)
    { 
        return _respawnStates[gameObject];
    }
    
}
