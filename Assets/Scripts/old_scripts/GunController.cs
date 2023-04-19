using System;
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
    private Camera _camera;
    
    public AudioSource gunAudio;
    public AudioClip gun1Sound;

    private void Start()
    {
        _camera= Camera.main;
    }

    void Update()
    {
        ScoreText.text = "Score: " + _score;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("pew");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (!hit.transform.CompareTag("Ammo"))
                {
                    Gun_1();
                }
            
                // Do something with the object that was hit by the raycast.
            
           
        }
    }
    
    void Gun_1()
    {
        gunAudio.clip = gun1Sound;
        gunAudio.Play();
    }
}
