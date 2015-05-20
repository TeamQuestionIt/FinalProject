using UnityEngine;
using System.Collections;
using System;

public class Character_Controller : MonoBehaviour
{

    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool facingRight = true;

    public Vector2 Direction { get; set; }

    private bool grounded = true;


    Animator anim;

    private Vector2 inputDirection = new Vector2();
    private Rigidbody2D rigidBody;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        //value from 1 (move right), -1 (move left)
         float move = inputDirection.x;

        anim.SetFloat("Speed", Mathf.Abs(move * maxSpeed));

        rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = anim.transform.localScale;
        newScale.x *= -1;
        anim.transform.localScale = newScale;
    }

    public void Jump()
    {
        if (grounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jumped");
            grounded = false;
        }
    }

    public void SetDirection(Vector2 input)
    {
        inputDirection = input.normalized;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //collided with edge collider on the background ie ground
        if (col.gameObject.name == "Street1")
        {
            grounded = true;
            //check if current animation state is falling
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                anim.SetTrigger("Landed");
            }
            
        }
    }

}
