using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DiskProperties : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _modelDestruct;
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private AudioSource _hitSound;

    [Space]
    [SerializeField] private float _speedRotation = 70.0f;
    [SerializeField] private float _durationRotation = 2.5f;
    [SerializeField] private float _durationStart = 3.0f;
    [SerializeField] private float _durationStop = 3.0f;

    public GameObject GetModel()
    {
        return _model;
    }

    public GameObject GetModelDestruct()
    {
        return _modelDestruct;
    }

    public GameObject GetHitEffect()
    {
        return _hitEffect;
    }

    public AudioSource GetHitSound()
    {
        return _hitSound;
    }

    public float GetSpeedRotation()
    {
        return _speedRotation;
    }

    public float GetDurationRotation()
    {
        return _durationRotation;
    }

    public float GetDurationStart()
    {
        return _durationStart;
    }

    public float GetDurationStop()
    {
        return _durationStop;
    }
}