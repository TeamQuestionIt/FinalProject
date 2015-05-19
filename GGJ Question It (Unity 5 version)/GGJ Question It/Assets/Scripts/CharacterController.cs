using UnityEngine;
using System.Collections;
using System;

public class CharacterController : MonoBehaviour {

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

	
	void Start () 
    {
        anim = GetComponentInChildren<Animator>();
	}
	
	void Update () 
    {
        if (jumpTimer > 0)
            jumpTimer--; //counts frames
	}

    public void FixedUpdate()
    {
        bool check = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(!grounded && check)
            anim.SetTrigger("Landed");
        grounded = check;
        anim.SetBool("Grounded", grounded);

        float move = inputDirection.x;

        anim.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

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
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
            jumpTimer = 5;
            anim.SetBool("Grounded", false);
            grounded = false;
        }
    }

    public void SetDirection(Vector2 input)
    {
        inputDirection = input;
    }
}
