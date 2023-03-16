using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    
    public Vector2 destination;
    
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AnimState();
        if (destination != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, (speed/1000));
        }
    }

    void AnimState()
    {
        //if gameobject is not moving turn on Target_Found trigger. use rb
        if (rb.velocity == Vector2.zero)
        {
            animator.SetTrigger("Target_Found");
        }
        else
        {
            animator.ResetTrigger("Target_Found");
        }
    }
}
