using UnityEngine;

public static class DataGame
{
    static private Texture _currentTexturesKnife;

    static private int _recordNumber;
    static private int _recordLevel;
    static private int _countApples;
    
    static private int _countScore;

    static public void SetCurrentTextureKnife(Texture currentTexture)
    {
        _currentTexturesKnife = currentTexture;
    }

    static public Texture GetCurrentTextureKnife()
    {
        return _currentTexturesKnife;
    }

    static public void Scoring(int level, int number)
    {
        if (_recordNumber < number)
        {
            PlayerPrefs.SetInt("recordNumber", number);
        }
        
        if (_recordLevel < level)
        {
            PlayerPrefs.SetInt("recordLevel", level);
        }
        
        PlayerPrefs.SetInt("countApples", _countApples);
    }

    static public int GetCountApples()
    {
        return _countApples;
    }

    static public void SetCountApples(int value)
    {
        _countApples = value;
    }

    static public void AddApple()
    {
        _countApples++;
    }

    static public void SetRecordNumber(int value)
    {
        _recordNumber = value;
    }

    static public void SetRecordLevel(int value)
    {
        _recordLevel = value;
    }

    static public int GetRecordNumber()
    {
        return _recordNumber;
    }

    static public int GetRecordLevel()
    {
        return _recordLevel;
    }
    
    static public void ResetScore()
    {
        _countScore = 0;
    }
    
    static public void AddScore(int value)
    {
        _countScore += value;
    }
    
    static public int GetScore()
    {
        return _countScore;
    }
}
