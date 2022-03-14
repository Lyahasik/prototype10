using UnityEngine;

public class RotationStopConst : RotationStop, IRotationStop
{
    public void StartRotation()
    {
        _isActive = true;
        
        _maxSpeed = _diskProperties.GetSpeedRotation();
        _currentSpeed = _maxSpeed;
        _timeNext = Time.time + _duration;
    }
    public void StopRotation()
    {
        _isActive = false;
    }
}