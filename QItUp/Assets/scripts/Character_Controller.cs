using UnityEngine;
using System.Collections;
using System;

public class Character_Controller : MonoBehaviour
{

    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    public bool facingRight = true;

    private bool onGround = true;
    private Animator anim;
    private Rigidbody2D rigidBody;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = anim.transform.localScale;
        newScale.x *= -1;
        anim.transform.localScale = newScale;
    }

    public void Move(float direction)
    {
        //this fixes bug, if direction was zero it locked x velocity to 0.
        if(direction == 0)
        {
            return;
        }
        rigidBody.velocity = new Vector2(direction * maxSpeed, rigidBody.velocity.y);
        //update animator, this line controls
        anim.SetFloat("Speed", Mathf.Abs(direction * maxSpeed));


        if (direction > 0 && !facingRight)
            Flip();
        else if (direction < 0 && facingRight)
            Flip();
    }

    public void Jump()
    {
        if (onGround)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jumped");
            onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //collided with edge collider on the background ie ground
        if (col.gameObject.name == "Street1")
        {
            onGround = true;
            //check if current animation state is falling
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                anim.SetTrigger("Landed");
            }

        }
    }

}
