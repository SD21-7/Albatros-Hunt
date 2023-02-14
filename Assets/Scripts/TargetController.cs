using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public bool MovingRight = true;
    private float _verticalmovement;

    [Range(0, 10)] public float _jumpforce;
    [Range(-10, 10)] public float speed;
    [Range(0, 5)] public float _jumprate;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        InvokeRepeating("Fly", 0, _jumprate);
    }

    private void Update()
    {
        _verticalmovement = _rigidbody2D.velocity.y;
        OnMouseOver();
        _rigidbody2D.velocity = new Vector2(speed, _verticalmovement);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    private void Fly()
    {
        _rigidbody2D.velocity = new Vector2(0, _jumpforce);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        speed = -speed;
        if (col.gameObject.CompareTag("TopCol"))
        {
            Destroy(gameObject);
        }
    }
}