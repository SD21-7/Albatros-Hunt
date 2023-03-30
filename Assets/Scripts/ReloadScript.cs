using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public class ReloadScript : MonoBehaviour
{
    public PlayerGun playerGun;
    public Gun gun;
    public GameObject Bullet;
   private Vector3 originalPos;
   private bool snapBack = true;
   private bool reloadable = true;

    private void Start()
    {
        playerGun = GameObject.FindWithTag("Player").GetComponent<PlayerGun>();
        // gun = GameObject.FindWithTag("Player").GetComponent<Gun>();
         originalPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && snapBack)
        {
            transform.position = originalPos;
        }
        if (transform.position == originalPos)
        {
            reloadable = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
     {
         if (col.gameObject.CompareTag("Gun") && reloadable)
         {
             reloadable = false;
             
             playerGun.GetGun().ChangeAmmo(1);
             Debug.Log(playerGun.GetGun().LoadedAmmo);
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
