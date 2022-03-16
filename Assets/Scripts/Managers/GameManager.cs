using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager;
    
    [SerializeField] private Texture[] _texturesKnife;

    private int _idCurrentTexture;

    private int _recordNumber;
    private int _recordLevel;
    private int _countApples;

    private void Start()
    {
        _uiManager = GetComponent<UIManager>();
        
        //TODO убрать в главную сцену
        _uiManager.GetSessionWindow().ResetScore(); 
        
        _idCurrentTexture = PlayerPrefs.GetInt("idCurrentTexture");
        _recordNumber = PlayerPrefs.GetInt("recordNumber");
        _recordLevel = PlayerPrefs.GetInt("recordLevel");
        _countApples = PlayerPrefs.GetInt("countApples");
        
        DataGame.SetCurrentTextureKnife(_texturesKnife[_idCurrentTexture]);
        DataGame.SetRecordNumber(_recordNumber);
        DataGame.SetRecordLevel(_recordLevel);
        DataGame.SetCountApples(_countApples);
        
        _uiManager.GetSessionWindow().UpdateApple();
    }

    public void SetIdCurrentTexture(int id)
    {
        _idCurrentTexture = id;
        
        DataGame.SetCurrentTextureKnife(_texturesKnife[_idCurrentTexture]);
    }
}
