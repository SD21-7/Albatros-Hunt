using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    public int _score;
    public int Target1Value;
    public int Target2Value;
    public int Target3Value;
    public TextMeshProUGUI ScoreText;
    
    public AudioSource gunAudio;
    public AudioClip gun1Sound;
    
    void Update()
    {
        ScoreText.text = "Score: " + _score;
        if (Input.GetMouseButtonDown(0))
        {
            Gun_1();
        }
    }
    
    void Gun_1()
    {
        gunAudio.clip = gun1Sound;
        gunAudio.Play();
    }
}
