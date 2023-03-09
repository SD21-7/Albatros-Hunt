using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class ReloadScript : MonoBehaviour
{
    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.tag == "Ammo")
    //     {
    //         Debug.Log("hoi");
    //     }
    // }
    
    Vector3 mousePositionOffset;
    
    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        mousePositionOffset = gameObject.transform.position - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition() + mousePositionOffset;
    }
    
    
}
