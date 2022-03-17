using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _sessionMenu;
    [SerializeField] private GameObject _gameOverMenu;
    
    private SessionWindow _sessionWindow;
    private GameOverWindow _gameOverWindow;

    private void Start()
    {
        if (_gameOverMenu)
        {
            _gameOverWindow = _gameOverMenu.GetComponent<GameOverWindow>();
        }
    }

    public SessionWindow GetSessionWindow()
    {
        if (!_sessionWindow)
        {
            _sessionWindow = _sessionMenu.GetComponent<SessionWindow>();
        }
        
        return _sessionWindow;
    }
    
    public GameOverWindow GetGameOverWindow()
    {
        return _gameOverWindow;
    }

    public void GameOver()
    {
        _gameOverWindow.SaveScore();

        Time.timeScale = 0.0f;
        
        _sessionMenu.SetActive(false);
        _gameOverMenu.SetActive(true);
    }
}
