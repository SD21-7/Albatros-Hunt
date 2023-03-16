using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer sprite;

    private float _verticalmovement;
    private bool _isdead = false;
    private bool goingRight;

    [SerializeField] [Range(0, 10)] private float _jumpforce;
    [SerializeField] [Range(-10, 10)] private float _speed;
    [SerializeField] [Range(0, 5)] private float _jumprate;
    [SerializeField] private bool isFlippable;
    
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        InvokeRepeating("Fly", 0, _jumprate);
        if (Random.Range(0, 2) == 1) goingRight = true;
        else if (isFlippable) sprite.flipX = !sprite.flipX;
    }

    private void Update()
    {
        if (!_isdead)
        {
            if (goingRight) _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
            else _rigidbody2D.velocity = new Vector2(-_speed, _rigidbody2D.velocity.y);
        }
    }

    private void Fly()
    {
        if (!_isdead)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpforce);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Collider"))
        {
            goingRight = !goingRight;
            if (isFlippable)
            {
                sprite.flipX = !sprite.flipX;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (col.gameObject.CompareTag("TopCol"))
        {
            Destroy(gameObject);
            //TODO: penalty for not hitting target
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Target"))
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
    }

    public IEnumerator Died()
    {
        Debug.Log("Died");
        _isdead = true;
        if (_animator != null) _animator.SetTrigger("Dead");
        //_rigidbody2D.velocity = new Vector2(0, 0);
        _rigidbody2D.velocity = new Vector2(0, 6);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}