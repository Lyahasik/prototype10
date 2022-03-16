using UnityEngine;

[RequireComponent(typeof(DiskProperties))]
[RequireComponent(typeof(IRotationStop))]
public abstract class DiskRotation : MonoBehaviour, IDiskRotation
{
    protected IRotationStop _iRotationStop;
    
    protected float _duration;
    protected float _speed;
    
    protected bool _isActive;
    protected float _timeNext;

    private void Awake()
    {
        _isActive = false;
    }

    private void Start()
    {
        _iRotationStop = GetComponent<IRotationStop>();
            
        _duration = GetComponent<DiskProperties>().GetDurationRotation();
        _speed = GetComponent<DiskProperties>().GetSpeedRotation();
    }
    
    void Update()
    {
        Rotation();
    }

    public void StartRotation()
    {
        _isActive = true;
        
        _timeNext = Time.time + _duration;
    }

    public void StopRotation()
    {
        _isActive = false;
    }
    
    protected virtual void Rotation()
    {
        if (_isActive)
        {
            transform.Rotate(Vector3.forward * _speed * Time.deltaTime);

            if (_timeNext < Time.time)
            {
                _isActive = false;
                _iRotationStop.StartRotation();
            }
        }
    }
}
