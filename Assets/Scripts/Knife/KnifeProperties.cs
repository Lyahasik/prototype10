using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProperties : MonoBehaviour
{
    [SerializeField] private float _speedThrow = 1000.0f;

    public float GetSpeedThrow()
    {
        return _speedThrow;
    }
}
