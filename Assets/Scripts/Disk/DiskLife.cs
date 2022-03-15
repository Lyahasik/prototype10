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
    private Collider _collider;
    private AudioSource _audioHit;
    
    [SerializeField] private SessionManager _sessionManager;

    private GameObject _model;
    private GameObject _modelDestruct;

    private List<ObjectImpulse> _listBaseKnifesImpulse = new List<ObjectImpulse>();
    private List<ObjectImpulse> _listKnifesImpulse = new List<ObjectImpulse>();
    private List<GameObject> _listApples = new List<GameObject>();
    
    private GameObject _hitEffect;
    private int _countGenerateKnife;
    private int _maxStuckKnife;
    
    private int _maxCountGenerateApple;

    public void SettingDisk()
    {
        _diskProperties = GetComponent<DiskProperties>();

        _model = _diskProperties.GetModel();
        _modelDestruct = _diskProperties.GetModelDestruct();
        
        _hitEffect = _diskProperties.GetHitEffect();
        _maxStuckKnife = _diskProperties.GetMaxStuckKnife();
        
        _maxCountGenerateApple = _diskProperties.GetMaxCountGenerateApple();

        _collider = GetComponent<Collider>();
        _audioHit = GetComponent<AudioSource>();
        
        _iRotationStart = GetComponent<IRotationStart>();

        StartCoroutine(GenerateSources());
        StartCoroutine(StartRotation());
    }

    private IEnumerator StartRotation()
    {
        yield return new WaitForSeconds(0.01f);
        
        _iRotationStart.StartRotation();
    }

    public bool HitKnife(Vector3 position)
    {
        GameObject knife = _sessionManager.GetKnife();
        
        _listKnifesImpulse.Add(knife.GetComponent<ObjectImpulse>());

        StartCoroutine(HitEffect(position));
        
        knife.transform.position = position;
        knife.transform.SetParent(transform, true);

        _maxStuckKnife--;
        if (_maxStuckKnife <= 0)
        {
            DiskDestruct();
            return false;
        }

        return true;
    }

    private IEnumerator HitEffect(Vector3 position)
    {
        _audioHit.Play();
        Instantiate(_hitEffect, position, _hitEffect.transform.rotation);
        
        transform.position += new Vector3(0, 0.05f, 0);

        yield return new WaitForSeconds(0.1f);
        
        transform.position += new Vector3(0, -0.05f, 0);
    }

    private void DiskDestruct()
    {
        foreach (ObjectImpulse objectImpulse in _listBaseKnifesImpulse)
        {
            objectImpulse.Impulse();
        }
        
        foreach (ObjectImpulse objectImpulse in _listKnifesImpulse)
        {
            objectImpulse.Impulse();
        }
        
        foreach (GameObject apple in _listApples)
        {
            apple.GetComponent<AppleLife>().SetInteract(false);
            apple.GetComponent<ObjectImpulse>().Impulse();
        }

        StartCoroutine(_sessionManager.NextDisk());
        
        _collider.enabled = false;
        _model.SetActive(false);
        
        _modelDestruct.SetActive(true);
    }

    public IEnumerator GenerateSources()
    {
        yield return new WaitForSeconds(0.01f);
        
        _countGenerateKnife = Random.Range(_diskProperties.GetMinCountGenerateKnife(), _diskProperties.GetMaxCountGenerateKnife() + 1);

        List<float> usedAngles = new List<float>();
        
        float step = 360.0f / _countGenerateKnife;
        float angle = 0.0f;
        
        for (int i = 0; i < _countGenerateKnife; i++)
        {
            GameObject knife = _sessionManager.GetBaseKnife();
            _listBaseKnifesImpulse.Add(knife.GetComponent<ObjectImpulse>());
            
            knife.transform.position = transform.position - new Vector3(0, 1.5f, 0);
            knife.transform.RotateAround(transform.position, Vector3.forward, angle);
            knife.transform.SetParent(transform, true);

            usedAngles.Add(angle);
            angle += step;
        }

        step = 360.0f / _maxCountGenerateApple;
        angle = step * 0.5f;
        
        for (int i = 0; i < _maxCountGenerateApple; i++)
        {
            bool cont = false;
            
            foreach (float usedAngle in usedAngles)
            {
                if (usedAngle + 15.0f > angle && usedAngle - 15.0f < angle)
                {
                    cont = true;
                    break;
                }
            }

            if (!cont
                && Random.Range(0.0f, 1.0f) <= _diskProperties.GetRandomGenerateApples())
            {
                GameObject apple = _sessionManager.GetApple();
                _listApples.Add(apple);

                apple.transform.position = transform.position - new Vector3(0, 1.645f, 0);
                apple.transform.eulerAngles = new Vector3(90.0f, -90.0f, 0.0f);
                apple.transform.RotateAround(transform.position, Vector3.forward, angle);
                apple.transform.SetParent(transform, true);
            }

            angle += step;
        }
    }
}
