using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public class ReloadScript : MonoBehaviour
{
   public GameObject Bullet;
    Vector3 originalPos;

    private void Start()
    {
        // originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
     {
         if (col.gameObject.tag == "Gun")
         { 
             AmmoUp();
               Bullet.transform.position = new Vector3(9.05f, -2.02f, 0f);
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
        // Get a reference to the GameObject's Transform component
        Transform transform = gameObject.GetComponent<Transform>();

        // Reset the position to (0, 0, 0)
        transform.position = Vector3.zero; 
    }
    
}
