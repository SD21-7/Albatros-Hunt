using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public bool MovingRight = true;
    private float _verticalmovement;
    private bool _isdead = false;

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
        if (!_isdead)
        {
            _verticalmovement = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = new Vector2(speed, _verticalmovement);
        }
    }

    private void OnMouseDown()
    {
        if (!_isdead)
        {
            StartCoroutine("Death");
        }
    }

    private void Fly()
    {
        if (!_isdead)
        {
            _rigidbody2D.velocity = new Vector2(0, _jumpforce);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       
        if (col.gameObject.CompareTag("Target"))
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }

        if (col.gameObject.CompareTag("LeftCol") || col.gameObject.CompareTag("RightCol"))
        {
            speed = -speed;
        }

        if (col.gameObject.CompareTag("TopCol"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Death()
    {
        _isdead = true;
        _rigidbody2D.velocity = new Vector2(0, 0);
        _rigidbody2D.velocity = new Vector2(0, _jumpforce);
        // yield return new WaitForSeconds(1f);
        // _rigidbody2D.velocity = new Vector2(0, -_jumpforce);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}