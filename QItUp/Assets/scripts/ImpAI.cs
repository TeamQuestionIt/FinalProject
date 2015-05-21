using UnityEngine;
using System.Collections;

public class ImpAI : MonoBehaviour
{

    public Vector2 jumpForce = new Vector2(10f, 250f);
    public Vector2 maxJumpVelocity = new Vector2(10f, 10f);
    public float JumpWaitTimer = 2f;
    //could randomize this a bit 1-5?
    private float timer = 0;

    private GameObject playerInstance;
    private Rigidbody2D rBody;
    private float xDirection = 0;



    // Use this for initialization
    void Start()
    {
        playerInstance = GameObject.Find("Player");
        rBody = GetComponent<Rigidbody2D>();
        timer = JumpWaitTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = JumpWaitTimer;
            Jump();
        }

        //debug
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
        }

    }

    private void Jump()
    {
        Debug.Log("jump");
        GetXDirection();
        Vector2 force = new Vector2(jumpForce.x * xDirection, jumpForce.y);
        rBody.AddForce(force);
    }

    private void GetXDirection()
    {
        if (playerInstance.transform.position.x > transform.position.x)
        {

            //need to go right
            xDirection = 1;
        }
        else
        {
            xDirection = -1;
        }
    }
}
