using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    Rigidbody2D rb;

    public List<Vector3> destinationList;
    public Vector3 destination;
    public Vector3 origin;
    public bool canMove;
    public bool pickingUp;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        origin = transform.position;
        destination = origin;
        animator.speed = 0;
        canMove = true;
        pickingUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationList.Count > 0 && canMove)
        {
            destination = destinationList[0];
            transform.position = Vector2.MoveTowards(transform.position, destination, (speed / 1000));
        }

        if (destination != origin)
        {
            animator.speed = 1;
        }

        AnimState();
    }

    void AnimState()
    {
        if (transform.position == destination && destination != origin)
        {
            Debug.Log("Target Found");
        }
        
        if (transform.position == destination && destination != origin && !pickingUp)
        {
            canMove = false;
            animator.SetBool("Target_Found", true);
        }

        if (transform.position == origin)
        {
            animator.speed = 0;
        }
    }

    private IEnumerator Pick_Up()
    {
        pickingUp = true;
        animator.SetBool("Target_Found", false);
        animator.SetBool("Pick_Up", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Pick_Up", false);
        sprite.enabled = false;
        transform.position = origin;
        yield return new WaitForSeconds(0.01f);
        destinationList.RemoveAt(0);
        sprite.enabled = true;
        canMove = true;
        pickingUp = false;
    }
}