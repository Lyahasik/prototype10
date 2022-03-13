using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    [SerializeField] private float _timeLife = 1.0f;
    void Start()
    {
        Destroy(gameObject, _timeLife);
    }
}
