﻿using UnityEngine;
using System.Collections;
using System;

public class Character_Controller : MonoBehaviour
{

    //public float MaxVelocity = 10f;
    //public float jumpForce = 700f;
    //public bool IsFacingRight = true;

    public Vector2 jumpForce = new Vector2(0, 450);
    public float maxVelocity = 2;
    public bool isFacingRight = true;


    private bool onGround = true;
    private Animator anim;
    private Rigidbody2D rBody;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        isFacingRight = true;
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = anim.transform.localScale;
        newScale.x *= -1;
        anim.transform.localScale = newScale;
    }

    public void Move(float direction)
    {
        //this fixes bug, if direction was zero it locked x velocity to 0.
        if (direction == 0)
        {
            return;
        }
        rBody.velocity = new Vector2(direction * maxVelocity, rBody.velocity.y);
        //update animator, this line controls
        //anim.SetFloat("Speed", Mathf.Abs(direction * maxVelocity));


        if (direction > 0 && !isFacingRight)
            Flip();
        else if (direction < 0 && isFacingRight)
            Flip();
    }

    public void Jump()
    {
        if (onGround)
        {
            rBody.AddForce(jumpForce);
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
