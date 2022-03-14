using UnityEngine;

public class RotationStopRandom : RotationStop, IRotationStop
{
    [SerializeField] private float _minDuration = 0.1f;
    [SerializeField] private float _maxDuration = 1.0f;
    
    public void StartRotation()
    {
        _isActive = true;
        
        _maxSpeed = _diskProperties.GetSpeedRotation();
        _currentSpeed = _maxSpeed;

        _duration = Random.Range(_minDuration, _maxDuration);
        _timeNext = Time.time + _duration;
    }
    
    public void StopRotation()
    {
        _isActive = false;
    }

    protected override void Rotation()
    {
        if (_isActive)
        {
            _delta = (_timeNext - Time.time) / _duration;
            
            _currentSpeed = Mathf.Lerp(0,_maxSpeed, _delta);
            
            if (_currentSpeed <= 0.0f)
            {
                _isActive = false;
                StartCoroutine(StartRotationStart());
                
                return;
            }
            
            transform.Rotate(Vector3.forward * _currentSpeed * Time.deltaTime);
        }
    }
}
