using System.Collections;
using UnityEngine;

[RequireComponent(typeof(KnifeProperties))]
[RequireComponent(typeof(ObjectImpulse))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class KnifeThrowing : MonoBehaviour
{
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private float _powerRotationDrop = 800f;
    
    private float _speed;
    private float _speedDrop;
    
    private ObjectImpulse _objectImpulse;
    private AudioSource _hitKnifeAudio;
    private Rigidbody _rigidbody;
    private Collider _collider;

    private Vector3 _startPosition;
    private bool _isThrow;
    
    void Start()
    {
        _speed = GetComponent<KnifeProperties>().GetSpeedThrow();
        _speedDrop = GetComponent<KnifeProperties>().GetSpeedDrop();

        _objectImpulse = GetComponent<ObjectImpulse>();
        _hitKnifeAudio = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _startPosition = transform.position;
        _isThrow = false;
    }

    public void Throw()
    {
        if (!_isThrow)
        {
            _isThrow = true;
            _rigidbody.AddForce(Vector3.up * _speed);
        }
    }

    private void HitDisk(DiskLife diskLife)
    {
        if (diskLife.HitKnife(transform.position))
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
        
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _isThrow = false;
        }
    }

    private void HitKnife()
    {
        StartCoroutine(_sessionManager.GameOver());
        _hitKnifeAudio.Play();
        
        Vibration.Vibrate(500);

        _collider.isTrigger = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _rigidbody.AddForce(-Vector3.up * _speedDrop);
        _rigidbody.AddTorque(transform.forward * _powerRotationDrop, ForceMode.Impulse);
    }

    public IEnumerator SplitDisk(float delayNext)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        
        _objectImpulse.Impulse();
        
        yield return new WaitForSeconds(delayNext);
        
        _collider.isTrigger = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;

        _isThrow = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        DiskLife diskLife = other.gameObject.GetComponent<DiskLife>();

        if (diskLife)
        {
            HitDisk(diskLife);
        }
        else
        {
            HitKnife();
        }
    }
}
