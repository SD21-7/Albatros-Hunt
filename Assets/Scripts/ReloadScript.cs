using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public class ReloadScript : MonoBehaviour
{
    Vector3 originalPos;

    private void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
     {
         if (col.gameObject.tag == "Gun")
         {
             AmmoUp();
               gameObject.transform.position = originalPos;
         }
     }
    
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

    private void AmmoUp()
    {
        
    }
    
}
