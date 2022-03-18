using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectKnife : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _managersController;
    private GameManager _gameManager;
    private HomeManager _homeManager;
    
    [SerializeField] private Texture _textureKnife;

    [Space] [SerializeField] private Image _imageBorder;

    private bool _isOpen = false;

    private void Start()
    {
        _gameManager = _managersController.GetComponent<GameManager>();
        _homeManager = _managersController.GetComponent<HomeManager>();
        
        CheckOpen();
    }

    private void CheckOpen()
    {
        _isOpen = _gameManager.CheckOpenKnife(_textureKnife);

        if (!_isOpen)
        {
            GetComponent<Image>().color = Color.black;
            _imageBorder.enabled = false;
        }
    }

    private void OnGUI()
    {
        CheckOpen();
        
        if (_isOpen)
        {
            if (_textureKnife == DataGame.GetCurrentTextureKnife())
            {
                _imageBorder.enabled = true;
            }
            else
            {
                _imageBorder.enabled = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isOpen)
        {
            _imageBorder.enabled = true;
            _homeManager.SwitchKnife(_textureKnife);
        }
    }
}
