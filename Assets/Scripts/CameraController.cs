﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public CinemachineBrain cameraBrain;

    private void Awake()
    {
        instance = this;
        cameraBrain = this.GetComponent<CinemachineBrain>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
