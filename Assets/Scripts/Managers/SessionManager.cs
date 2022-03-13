using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainKnife;
    [SerializeField] private Texture _textureKnife;
    
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
        ApplyMaterialKnifes();
        ResetIssued();
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

    public GameObject GetKnife()
    {
        if (_issuedKnifes < _pullKnifes.Length)
        {
            return _pullKnifes[_issuedKnifes++];
        }
        
        Debug.Log("Error: not knifes in pull.");
        return null;
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
            knife.transform.localPosition = Vector3.zero;
            knife.transform.rotation = Quaternion.identity;
        }
        
        foreach (GameObject knife in _pullKnifes)
        {
            knife.transform.SetParent(_objectPullKnifes.transform, true);
            knife.transform.localPosition = Vector3.zero;
            knife.transform.rotation = Quaternion.identity;
        }
        
        foreach (GameObject apple in _pullApples)
        {
            apple.transform.SetParent(_objectPullApples.transform, true);
            apple.transform.localPosition = Vector3.zero;
            apple.transform.rotation = Quaternion.identity;
        }
        
        _issuedKnifes = 0;
        _issuedApples = 0;
    }

    private void ApplyMaterialKnifes()
    {
        _mainKnife.GetComponent<MeshRenderer>().material.mainTexture = _textureKnife;
        
        foreach (GameObject knife in _pullKnifes)
        {
            knife.GetComponent<MeshRenderer>().material.mainTexture = _textureKnife;
        }
    }
}
