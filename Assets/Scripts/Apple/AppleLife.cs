using UnityEngine;

public class AppleLife : MonoBehaviour
{
    [SerializeField] private SessionManager _sessionManager;
    
    [Space]
    [SerializeField] private GameObject _prefabDestroy;

    private GameObject _objectPullApples;
        
    private AudioSource _audioSource;

    private bool _isInteract = true;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetInteract(bool value)
    {
        _isInteract = value;
    }

    public void SetObjectPullApples(GameObject objectPullApples)
    {
        _objectPullApples = objectPullApples;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isInteract)
        {
            DataGame.AddApple();
            _sessionManager.GetUIManager().GetSessionWindow().UpdateApple();
            
            Instantiate(_prefabDestroy, transform.position, transform.rotation);
        
            transform.SetParent(_objectPullApples.transform, true);
            transform.localPosition = Vector3.zero;
        
            _audioSource.Play();
        }
    }
}
