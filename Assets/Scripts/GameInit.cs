using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    public GameObject CrossHair;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CrossHair.transform.position = new Vector3(mousePos.x, mousePos.y, 100);
    }
}
