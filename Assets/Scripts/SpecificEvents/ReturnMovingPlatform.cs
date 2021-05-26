﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Aux_Classes;
using UnityEngine;

public class ReturnMovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float returnPoint_back;
    [Space] 
    [Header("DEBUGGING")] 
    [SerializeField] private float lineLength;

    [Space] 
    [Header("ARRUMAR ESSA PORRA DEPOIS")] 
    [SerializeField]
    private GameObject associatedLight;
    

    // private Animator _anim;
    private HorizontalPlatform _responder;
    
    void Awake()
    {
        // _anim = GetComponent<Animator>();
        _responder = GetComponent<HorizontalPlatform>();
        returnPoint_back *= Mathf.Sign(transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.position.x) < Math.Abs(returnPoint_back)
            && transform.position.y > 9)
        {
            // _anim.SetTrigger("Off");
            // _responder.CanReturn = true;
            // _responder.LERPReturn();
        }

        // if (_anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        // {
        //     associatedLight.SetActive(true);
        // }

        if (!_responder.IsInterpolating)
        {
            associatedLight.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(returnPoint_back,-lineLength),new Vector2(returnPoint_back,lineLength));
    }
}
