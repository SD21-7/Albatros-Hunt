using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int Score;
    [SerializeField] private TextMeshProUGUI text;

    public void EnemyDied(int modifyBy)
    {
        Score += modifyBy;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + Score;
    }
    
    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScore", Score);
    }
}