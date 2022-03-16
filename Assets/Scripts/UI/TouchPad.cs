using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private SessionManager _sessionManager;

    private KnifeThrowing _knifeThrowing;

    private void Start()
    {
        _knifeThrowing = _sessionManager.GetMainKnife().GetComponent<KnifeThrowing>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _knifeThrowing.Throw();
    }
}
