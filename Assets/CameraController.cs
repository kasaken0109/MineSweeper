using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    [SerializeField] private float m_shakeXpos = 0.3f;
    [SerializeField] private float m_shakeYpos = 0.3f;
    [SerializeField] private float m_shakeTime = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeScreen()
    {
        
    }
}
