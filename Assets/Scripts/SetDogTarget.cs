using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDogTarget : MonoBehaviour
{
    DogController dogController;

    private void Start()
    {
        dogController = GameObject.Find("Dog").GetComponent<DogController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target") && collision.gameObject.GetComponent<TargetController>()._isdead == true)
        {
            Debug.Log("Target hit");
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<Animator>().speed = 0;
            dogController.destinationList.Add(collision.gameObject.transform.position);
        }
    }
    
}
