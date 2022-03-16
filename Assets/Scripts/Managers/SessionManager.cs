using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    private UIManager _uiManager;

    [Space]
    [SerializeField] private float _delayNext = 1.0f;

    [SerializeField] private GameObject _objectStorage;
    [SerializeField] private GameObject[] _disks;
    
    private GameObject _currentDisk;
    private int _currentIdDisk = 0;
    private IDiskRotation _iDiskRotation;
    private IRotationStart _iRotationStart;
    private IRotationStop _iRotationStop;
    
    [SerializeField] private GameObject _mainKnife;
    private KnifeThrowing _knifeThrowing;
    
    [Space]
    [SerializeField] private GameObject _objectPullBaseKnifes;
    [SerializeField] private GameObject[] _pullBaseKnifes;
    private int _issuedBaseKnifes;
    
    [Space]
    [SerializeField] private GameObject _objectPullKnifes;
    [SerializeField] private GameObject[] _pullKnifes;
    private int _issuedKnifes;
    
    [Space]
    [SerializeField] private GameObject _objectPullApples;
    [SerializeField] private GameObject[] _pullApples;
    private int _issuedApples;

    private void Start()
    {
        _uiManager = GetComponent<UIManager>();
            
        _knifeThrowing = _mainKnife.GetComponent<KnifeThrowing>();
        
        InitDisk();
        ApplyMaterialKnifes();
        ResetIssued();
    }

    public UIManager GetUIManager()
    {
        return _uiManager;
    }

    public GameObject GetMainKnife()
    {
        return _mainKnife;
    }

    void InitDisk()
    {
        if (_currentIdDisk == _disks.Length)
        {
            PlayerPrefs.SetInt("countApples", DataGame.GetCountApples());

            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
            return;
        }
        
        _currentDisk = _disks[_currentIdDisk];
        
        _iDiskRotation = _currentDisk.GetComponent<IDiskRotation>();
        _iRotationStart = _currentDisk.GetComponent<IRotationStart>();
        _iRotationStop = _currentDisk.GetComponent<IRotationStop>();
        
        _currentDisk.transform.SetParent(_objectStorage.transform, true);
        _currentDisk.transform.position = new Vector3(0.0f, 0.9f, 0.0f);
        _currentDisk.GetComponent<DiskLife>().SettingDisk();

        _currentIdDisk++;
    }

    public GameObject GetObjectPullBaseKnifes()
    {
        return _objectPullBaseKnifes;
    }

    public GameObject GetBaseKnife()
    {
        if (_issuedBaseKnifes < _pullBaseKnifes.Length)
        {
            return _pullBaseKnifes[_issuedBaseKnifes++];
        }
        
        Debug.Log("Error: not base knifes in pull.");
        return null;
    }

    public GameObject GetObjectPullKnifes()
    {
        return _objectPullKnifes;
    }

    public GameObject GetKnife()
    {
        if (_issuedKnifes < _pullKnifes.Length)
        {
            return _pullKnifes[_issuedKnifes++];
        }
        
        Debug.Log("Error: not knifes in pull.");
        return null;
    }

    public GameObject GetObjectPullApples()
    {
        return _objectPullApples;
    }

    public GameObject GetApple()
    {
        if (_issuedApples < _pullApples.Length)
        {
            return _pullApples[_issuedApples++];
        }
        
        Debug.Log("Error: not apples in pull.");
        return null;
    }

    public void ResetIssued()
    {
        foreach (GameObject knife in _pullBaseKnifes)
        {
            knife.transform.SetParent(_objectPullBaseKnifes.transform, true);

            Rigidbody rigidbody = knife.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;

            knife.transform.localPosition = Vector3.zero;
            knife.transform.rotation = Quaternion.identity;
            
            knife.GetComponent<Collider>().isTrigger = false;
        }
        
        foreach (GameObject knife in _pullKnifes)
        {
            knife.transform.SetParent(_objectPullKnifes.transform, true);

            Rigidbody rigidbody = knife.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;
            
            knife.transform.localPosition = Vector3.zero;
            knife.transform.rotation = Quaternion.identity;
            
            knife.GetComponent<Collider>().isTrigger = false;
        }

        int i = 1;
        foreach (GameObject apple in _pullApples)
        {
            AppleLife appleLife = apple.GetComponent<AppleLife>();
            
            appleLife.SetObjectPullApples(_objectPullApples);
            appleLife.SetInteract(true);
            
            Rigidbody rigidbody = apple.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

            apple.transform.SetParent(_objectPullApples.transform, true);
            apple.transform.localPosition = new Vector3(0.0f, i++, 0.0f);
            apple.transform.rotation = Quaternion.identity;
        }

        _issuedBaseKnifes = 0;
        _issuedKnifes = 0;
        _issuedApples = 0;
    }

    private void ApplyMaterialKnifes()
    {
        _mainKnife.GetComponent<MeshRenderer>().material.mainTexture = DataGame.GetCurrentTextureKnife();
        
        foreach (GameObject knife in _pullKnifes)
        {
            knife.GetComponent<MeshRenderer>().material.mainTexture = DataGame.GetCurrentTextureKnife();
        }
    }

    public IEnumerator GameOver()
    {
        StopRotate();

        yield return new WaitForSeconds(_delayNext);

        PlayerPrefs.SetInt("countApples", DataGame.GetCountApples());
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator NextDisk()
    {
        _uiManager.GetSessionWindow().AddScore(2);
        _uiManager.GetSessionWindow().NextDisk();
        
        StartCoroutine(_knifeThrowing.SplitDisk(_delayNext));
        
        StopRotate();

        yield return new WaitForSeconds(_delayNext);

        ResetIssued();
        
        yield return new WaitForSeconds(0.05f);
        
        Destroy(_currentDisk);
        InitDisk();
    }

    private void StopRotate()
    {
        _iDiskRotation.StopRotation();
        _iRotationStart.StopRotation();
        _iRotationStop.StopRotation();
    }
}
