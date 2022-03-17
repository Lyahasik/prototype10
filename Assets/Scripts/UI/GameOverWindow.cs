using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [Space] [SerializeField] private Text _countApples;
    [Space] [SerializeField] private Text _countScore;
    [Space] [SerializeField] private Text _disks;

    private void Start()
    {
        _countApples.text = DataGame.GetCountApples().ToString();
        
        _countScore.text = DataGame.GetScore().ToString();
        _disks.text = "disk " + DataGame.GetDisk();
        
        DataGame.ResetScore();
    }

    public void SaveScore()
    {
        DataGame.Scoring();
    }
}
