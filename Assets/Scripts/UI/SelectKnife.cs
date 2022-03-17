using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectKnife : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private HomeManager _homeManager;
    [SerializeField] private Texture _textureKnife;

    [Space] [SerializeField] private Image _imageBorder;

    private void OnGUI()
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

    public void OnPointerDown(PointerEventData eventData)
    {
        _imageBorder.enabled = true;
        _homeManager.SwitchKnife(_textureKnife);
    }
}
