using UnityEngine;

public class RotationStopConst : RotationStop, IRotationStop
{
    public void Switch()
    {
        _isActive = !_isActive;
        
        _maxSpeed = _diskProperties.GetSpeedRotation();
        _currentSpeed = _maxSpeed;
        _timeNext = Time.time + _duration;
    }
}