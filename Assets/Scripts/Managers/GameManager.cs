using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SessionManager _sessionManager;
    
    [SerializeField] private List<Texture> _texturesKnife;

    private int _idCurrentTexture;

    private int _recordNumber;
    private int _recordLevel;
    private int _countApples;

    private void Start()
    {
        _sessionManager = GetComponent<SessionManager>();
            
        _idCurrentTexture = PlayerPrefs.GetInt("idCurrentTexture");
        _recordNumber = PlayerPrefs.GetInt("recordNumber");
        _recordLevel = PlayerPrefs.GetInt("recordLevel");
        _countApples = PlayerPrefs.GetInt("countApples");
        
        DataGame.SetCurrentTextureKnife(_texturesKnife[_idCurrentTexture]);
        DataGame.SetRecordNumber(_recordNumber);
        DataGame.SetRecordLevel(_recordLevel);
        DataGame.SetCountApples(_countApples);

        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            _sessionManager.StartSession();
            GetComponent<UIManager>().GetSessionWindow().UpdateApple();
        }
        else
        {
            GetComponent<HomeManager>().SetDataAcc();
        }
    }

    public void SetIdCurrentTexture(Texture texture)
    {
        _idCurrentTexture = _texturesKnife.IndexOf(texture);
        
        PlayerPrefs.SetInt("idCurrentTexture", _idCurrentTexture);
        DataGame.SetCurrentTextureKnife(_texturesKnife[_idCurrentTexture]);
    }
}
