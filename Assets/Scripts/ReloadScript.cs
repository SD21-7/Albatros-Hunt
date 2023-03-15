using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public class ReloadScript : MonoBehaviour
{
    public PlayerGun playerGun;
    public GameObject Bullet;
   private Vector3 originalPos;

    private void Start()
    {
        playerGun = GameObject.FindWithTag("Player").GetComponent<PlayerGun>();
         originalPos = transform.position;
    }

    private void OnTriggerStay2D(Collider2D col)
     {
         if (col.gameObject.tag == "Gun")
         { 
             if (Input.GetMouseButtonUp(0))
             {
                 AmmoUp();
                 transform.position = originalPos;
             }
            
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

    private void OnMouseOver()
    {
        playerGun.canFire = false;
    }
    
    private void OnMouseExit()
    {
        playerGun.canFire = true;
    }

    private void AmmoUp()
    {
        Debug.Log("hugb");
    }
    
}
