using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class fps : MonoBehaviour
{
    public TextMeshProUGUI fpscounter;
    
    void Update()
    {
        //display fps
        float fps = 1.0f / Time.deltaTime;
        fpscounter.text = "FPS: " + fps.ToString("F0");
    }
}
