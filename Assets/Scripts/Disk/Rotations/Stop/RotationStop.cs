using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DiskProperties))]
public class RotationStop : MonoBehaviour
{
    private IRotationStart _iRotationStart;
    
    protected float _duration;
    protected float _maxSpeed;
    protected float _currentSpeed;
    
    protected bool _isActive;
    protected float _delta;
    protected float _timeNext;

    protected DiskProperties _diskProperties;

    private void Awake()
    {
        _isActive = false;
    }

    private void Start()
    {
        _iRotationStart = (IRotationStart) GetComponent(typeof(IRotationStart));
        
        _duration = GetComponent<DiskProperties>().GetDurationStop();
        _diskProperties = GetComponent<DiskProperties>();
    }

    void Update()
    {
        Rotation();
    }

    protected virtual void Rotation()
    {
        if (_isActive)
        {
            _delta = (_timeNext - Time.time) / _duration;
            
            _currentSpeed = Mathf.Lerp(0,_maxSpeed, _delta);
            
            if (_currentSpeed <= 0.0f)
            {
                _isActive = false;
                StartCoroutine(StartRotation());
                
                return;
            }
            
            transform.Rotate(Vector3.forward * _currentSpeed * Time.deltaTime);
        }
    }
    
    protected IEnumerator StartRotation()
    {
        yield return new WaitForSeconds(0.5f);
        
        _iRotationStart.Switch();
    }
}
