using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SessionWindow _sessionWindow;

    public SessionWindow GetSessionWindow()
    {
        return _sessionWindow;
    }
}
