using UnityEngine;
using System.Collections;

public class Enemy_Imp : MonoBehaviour {

    public BoxCollider2D hitBox;
    public GameObject playerInstance;
    public uint damage = 10;
    public Vector2 jumpForce = new Vector2(25f, 250f);
    public float dxMultiplier = 10f;
    public Vector2 maxJumpVelocity = new Vector2(5f, 55f);
    public float JumpWaitTimer = 2f;

    //these are for trying different flavors during development phase
    public bool useVariableJumpDistance = true;
    public bool useVariableJumpTimer = true;

    private float timer = 0;
    
    private Rigidbody2D rBody;
    private float xDirection = 0;
    private bool onGround = false;
    private bool isFacingRight = false;

    public bool IsHitBox(Collider2D col)
    {
        BoxCollider2D b = col as BoxCollider2D;
        if (null == b)
        {
            return false;
        }
        return b.size == hitBox.size && b.offset == hitBox.offset;
    }

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        timer = JumpWaitTimer;
	}

    private void FixedUpdate()
    {
	 if (onGround)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (useVariableJumpTimer)
                {
                    timer = Random.Range(0f, 5f);
                    //Debug.Log(timer);
                }
                else
                {
                    timer = JumpWaitTimer;
                }

                Jump();
            }
        }
	}

    private void Jump()
    {
        //Debug.Log("jump");
        onGround = false;
        GetXDirection();
        float dxToPlayer = Vector2.Distance(transform.position, playerInstance.transform.position);


        Vector2 force;
        if (useVariableJumpDistance)
        {
            force = new Vector2((jumpForce.x / dxToPlayer) * dxMultiplier * xDirection, jumpForce.y);
        }
        else
        {
            force = new Vector2(jumpForce.x * xDirection, jumpForce.y);
        }

        //Debug.Log(force);
        rBody.AddForce(force);
    }



    private void GetXDirection()
    {
        if (playerInstance.transform.position.x > transform.position.x)
        {

            //need to go right
            xDirection = 1;
            if (!isFacingRight)
            {
                Flip();
                isFacingRight = true;
            }

        }
        else
        {
            xDirection = -1;
            if (isFacingRight)
            {
                Flip();
                isFacingRight = false;
            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //hacky should do this better, probably use a ground tag
        if (col.gameObject.name == "Street1")
        {
            onGround = true;
        }
    }
}
