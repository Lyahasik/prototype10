using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectImpulse : MonoBehaviour
{
    [SerializeField] private float _powerTorque = 1000.0f;
    [SerializeField] private float _powerForce = 300.0f;
    [SerializeField] private bool _isStart = false;
    [SerializeField] private bool _axisZ = false;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        
        if (_isStart)
        {
            Impulse();
        }
    }

    public void Impulse()
    {
        if (_axisZ)
        {
            _collider.isTrigger = true;
            _rigidbody.isKinematic = false;
            
            _rigidbody.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0) * _powerForce);
            _rigidbody.AddTorque(transform.forward * _powerTorque, ForceMode.Impulse);
        }
        else
        {
            _rigidbody.AddForce(new Vector3(
                Random.Range(-_powerTorque, _powerTorque), 
                Random.Range(-_powerTorque, _powerTorque), 
                Random.Range(-_powerTorque, _powerTorque)));
            
            _rigidbody.AddTorque(new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
        }
    }
}
