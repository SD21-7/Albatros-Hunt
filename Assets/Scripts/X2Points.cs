using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2Points : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("X2Points");
        Destroy(gameObject);
    }
}
