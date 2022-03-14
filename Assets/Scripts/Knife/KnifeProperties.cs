using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProperties : MonoBehaviour
{
    [SerializeField] private float _speedThrow = 1000.0f;
    [SerializeField] private float _speedDrop = 200.0f;

    public float GetSpeedThrow()
    {
        return _speedThrow;
    }
    
    public float GetSpeedDrop()
    {
        return _speedDrop;
    }
}
