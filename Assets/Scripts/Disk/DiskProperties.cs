using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DiskProperties : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _modelDestruct;
    [SerializeField] private GameObject _hitEffect;

    [Space]
    [SerializeField] private float _speedRotation = 70.0f;
    [SerializeField] private float _durationRotation = 2.5f;
    [SerializeField] private float _durationStart = 3.0f;
    [SerializeField] private float _durationStop = 3.0f;

    [SerializeField] private int _minCountGenerateKnife = 0;
    [SerializeField] [Range(1, 6)] private int _maxCountGenerateKnife = 1;
    [SerializeField] private int _maxStuckKnife = 6;
    
    [SerializeField] [Range(1, 6)] private int _maxCountGenerateApple = 1;
    [SerializeField] [Range(0.0f, 1.0f)] private float _randomGenerateApples = 0.25f;

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

    public int GetMinCountGenerateKnife()
    {
        return _minCountGenerateKnife;
    }

    public int GetMaxCountGenerateKnife()
    {
        return _maxCountGenerateKnife;
    }

    public int GetMaxStuckKnife()
    {
        return _maxStuckKnife;
    }

    public int GetMaxCountGenerateApple()
    {
        return _maxCountGenerateApple;
    }

    public float GetRandomGenerateApples()
    {
        return _randomGenerateApples;
    }
}