using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiskProperties))]
[RequireComponent(typeof(IDiskRotation))]
[RequireComponent(typeof(IRotationStart))]
[RequireComponent(typeof(IRotationStop))]
public class DiskLife : MonoBehaviour
{
    private DiskProperties _diskProperties;
    private IRotationStart _iRotationStart;
    private AudioSource _audioHit;
    
    [SerializeField] private SessionManager _sessionManager;

    private GameObject _hitEffect;
    private int _countGenerateKnife;
    private int _maxStuckKnife;

    void Start()
    {
        _diskProperties = GetComponent<DiskProperties>();
        _maxStuckKnife = _diskProperties.GetMaxStuckKnife();
        _hitEffect = _diskProperties.GetHitEffect();
        
        _audioHit = GetComponent<AudioSource>();
        
        _iRotationStart = (IRotationStart) GetComponent(typeof(IRotationStart));

        GenerateSources();
        StartCoroutine(StartRotation());
    }

    private IEnumerator StartRotation()
    {
        yield return new WaitForSeconds(0.1f);
        
        _iRotationStart.Switch();
    }

    public void HitKnife(Vector3 position)
    {
        GameObject knife = _sessionManager.GetKnife();
        
        _audioHit.Play();
        Instantiate(_hitEffect, position, _hitEffect.transform.rotation);
        
        knife.transform.position = position;
        knife.transform.SetParent(transform, true);
    }

    private void GenerateSources()
    {
        _countGenerateKnife = Random.Range(_diskProperties.GetMinCountGenerateKnife(), _diskProperties.GetMaxCountGenerateKnife() + 1);
        
        float step = 360.0f / _countGenerateKnife;
        float angle = Random.Range(0.0f, step);
        
        for (int i = 0; i < _countGenerateKnife; i++)
        {
            GameObject knife = _sessionManager.GetBaseKnife();
        
            knife.transform.position = transform.position - new Vector3(0, 1.5f, 0);
            knife.transform.RotateAround(transform.position, Vector3.forward, angle);
            knife.transform.SetParent(transform, true);

            angle += step;
        }
    }
}
