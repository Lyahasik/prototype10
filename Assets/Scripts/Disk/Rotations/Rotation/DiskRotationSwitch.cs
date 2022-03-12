using System.Diagnostics;
using UnityEngine;

public class DiskRotationSwitch : DiskRotation
{
    [SerializeField] private float _switchInterval = 1.0f;
    
    private int _rezus = 1;
    private float _delay = 0.0f;

    protected override void Rotation()
    {
        if (_isActive)
        {
            transform.Rotate(Vector3.forward * _rezus * _speed * Time.deltaTime);
        }
        
        if (_delay < Time.time)
        {
            _delay = Time.time + _switchInterval;
            _rezus = -_rezus;
        }
    }
}
