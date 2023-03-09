using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private GunController _gunController;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer sprite;
    public GameObject Crosshair;

    
    private float _verticalmovement;
    private bool _isdead = false;

    [Range(0, 10)] public float _jumpforce;
    [Range(-10, 10)] public float _speed;
    [Range(0, 5)] public float _jumprate;

    private void Start()
    {
        Crosshair = GameObject.Find("Crosshair");
        sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _gunController = Crosshair.GetComponent<GunController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        InvokeRepeating("Fly", 0, _jumprate);
    }

    private void Update()
    {
        if (!_isdead)
        {
            _verticalmovement = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = new Vector2(_speed, _verticalmovement);
        }
    }

    private void OnMouseDown()
    {
        if (!_isdead)
        {
            StartCoroutine("Death");
            if (gameObject.name == "Target 1(Clone)")
            {
                _gunController._score += _gunController.Target1Value;
            }
            else if (gameObject.name == "Target 2(Clone)")
            {
                _gunController._score += _gunController.Target2Value;
            }
            else if (gameObject.name == "Target 3(Clone)")
            {
                _gunController._score += _gunController.Target3Value;
            }
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
            _speed = -_speed;
            sprite.flipX = !sprite.flipX;
        }

        if (col.gameObject.CompareTag("TopCol"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Death()
    {
        _isdead = true;
        _animator.SetTrigger("Dead");
        _rigidbody2D.velocity = new Vector2(0, 0);
        _rigidbody2D.velocity = new Vector2(0, 6);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}