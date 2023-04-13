using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI _titleText;
    public GameObject _LevelButton;
    public TextMeshProUGUI _HighScoreText;
    
    public void PlayGame()
    {
        PlayerPrefs.SetString("Gun", "HuntRifle");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Start()
    {
        InvokeRepeating("TitleColor", 0f, 0.1f);
        if (PlayerPrefs.HasKey("CanSelect"))
        {
            if (PlayerPrefs.GetInt("CanSelect") == 1)
            {
                _LevelButton.SetActive(true);
            }
            else
            {
                _LevelButton.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("CanSelect", 0);
            _LevelButton.SetActive(false);
        }

        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (PlayerPrefs.GetInt("HighScore") > 0)
            {
                _HighScoreText.text = "Highscore:" + PlayerPrefs.GetInt("HighScore");
            }
            else
            {
                _HighScoreText.text = "";
            }
        }
        else
        {
            _HighScoreText.text = "";
        }
    }

    public void TitleColor()
    {
        _titleText.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }
}
