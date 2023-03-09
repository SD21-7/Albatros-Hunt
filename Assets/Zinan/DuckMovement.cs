using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DuckMovement : MonoBehaviour
{
    [SerializeField] private float jumpTimer;
    [SerializeField] private float jumpForce;
    [SerializeField] private float horizontalForce;
    private float jumpCounter;
    private bool isRight = false;


    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void RandomiseValues()
    {
        jumpTimer += new Random().Next(-5, 5) / 20f;
        jumpForce += new Random().Next(-5, 5) / 20f;
        horizontalForce += new Random().Next(-5, 5) / 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight) rb.velocity = new Vector2(horizontalForce, rb.velocity.y);
        else rb.velocity = new Vector2(-horizontalForce, rb.velocity.y);
        if (jumpCounter > 0) jumpCounter -= Time.deltaTime;
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("flap");
            jumpCounter = jumpTimer;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Collider"))
        {
            isRight = !isRight;
        }
    }
}
