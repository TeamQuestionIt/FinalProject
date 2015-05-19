using UnityEngine;
using System.Collections;
using System;

public class Character_Controller : MonoBehaviour {

    public float maxSpeed = 10f;
    public float jumpForce = 700f;

    bool grounded = true;
    public Transform groundCheck;
    float groundRadius = .2f;
    public LayerMask whatIsGround;
    private int jumpTimer = 0;

    public bool facingRight = true;
    Animator anim;

    private Vector2 inputDirection = new Vector2();
    private Rigidbody2D rigidBody;

	void Start () 
    {
        anim = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
    {
        
	}

    public void FixedUpdate()
    {
        if (jumpTimer > 0)
            jumpTimer--; //counts frames

        bool check = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (!grounded && check && rigidBody.velocity.y < 0)
            anim.SetTrigger("Landed");
        grounded = check;
        anim.SetBool("Grounded", grounded);

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
        if (grounded && jumpTimer <= 0)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            jumpTimer = 5;
            anim.SetBool("Grounded", false);
            anim.SetTrigger("Jumped");
            grounded = false;
        }
    }

    public void SetDirection(Vector2 input)
    {
        inputDirection = input.normalized;
    }

}
