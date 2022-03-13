using UnityEngine;

[RequireComponent(typeof(KnifeProperties))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class KnifeThrowing : MonoBehaviour
{
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private float _powerRotationDrop = 0.5f;
    
    private float _speed;
    
    private AudioSource _hitKnifeAudio;
    private Rigidbody _rigidbody;
    private Collider _collider;

    private Vector3 _startPosition;
    private bool _isThrow;
    void Start()
    {
        _speed = GetComponent<KnifeProperties>().GetSpeedThrow();
        _hitKnifeAudio = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _startPosition = transform.position;
        _isThrow = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Throw();
        }
    }

    public void Throw()
    {
        if (!_isThrow)
        {
            _isThrow = true;
            _rigidbody.AddForce(Vector3.up * _speed);
        }
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

    private void HitDisk(DiskLife diskLife)
    {
        diskLife.HitKnife(transform.position);

        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _isThrow = false;
    }

    private void HitKnife()
    {
        StartCoroutine(_sessionManager.GameOver());
        _hitKnifeAudio.Play();

        _collider.isTrigger = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _rigidbody.useGravity = true;
        _rigidbody.AddTorque(transform.forward * _powerRotationDrop, ForceMode.Impulse);
    }
}
