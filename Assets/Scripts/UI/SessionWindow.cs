using UnityEngine;
using UnityEngine.UI;

public class SessionWindow : MonoBehaviour
{
    [SerializeField] private  Image[] _disks;
    [SerializeField] private  Image[] _knifes;

    [Space] [SerializeField] private Text _countScore;
    [Space] [SerializeField] private Text _countApples;

    private int _currentDisk;
    private int _countStuckKnife;
    
    private int _number;

    public void SetCountStuckKnife(int value)
    {
        _countStuckKnife = value;

        int i = 0;
        foreach (Image knife in _knifes)
        {
            if (i < _countStuckKnife)
            {
                knife.enabled = true;
                i++;
            }
            else
            {
                knife.enabled = false;
            }
        }
    }

    public void Hit()
    {
        _countStuckKnife--;

        _knifes[_countStuckKnife].enabled = false;
    }

    public void UpdateApple()
    {
        _countApples.text = DataGame.GetCountApples().ToString();
    }

    public void ResetScore()
    {
        DataGame.ResetScore();
        
        _countScore.text = DataGame.GetScore().ToString();
    }

    public void AddScore(int value)
    {
        DataGame.AddScore(value);
        
        _countScore.text = DataGame.GetScore().ToString();
    }

    public void NextDisk()
    {
        _disks[_currentDisk].color = new Color(1.0f, 0.6901961f, 0.07843138f);
            
        _currentDisk++;
    }
}
