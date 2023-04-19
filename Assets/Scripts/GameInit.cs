using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    public GameObject CrossHair;
    public GameObject PausePanel;
    public bool isPaused;
    public GameObject Crosshair;

    private void Start()
    {
        Cursor.visible = false;
        //lock fps to 30
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CrossHair.transform.position = new Vector3(mousePos.x, mousePos.y, 100);
        
        //if press esc pause game and show paused panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                isPaused = true;
                Time.timeScale = 0;
                PausePanel.SetActive(true);
                Crosshair.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
                PausePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Crosshair.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
    }
}
