using UnityEngine;
using System.Collections;

public class ImpAI : MonoBehaviour
{
    public Vector2 jumpForce = new Vector2(10f, 250f);
    public float attackDx = 0;//maximum distance from player to start attack
    public float dxMultiplier = 0f;//needed if using variable jumping
    public Vector2 maxJumpVelocity;
    public float JumpWaitTimer = 0;
    public int hitPoints = 0;
    public int currentHitPoints = 0;
    public int scoreValue = 0;
    public int damage = 0;
    public bool useVariableJumpDistance = true;
    public bool useVariableJumpTimer = true;
    public bool useConstantAttackAnimation = false;

    //could randomize this a bit 1-5?
    private float timer = 0;

    private GameObject playerInstance;
    private Player playerScript;
    private Rigidbody2D rBody;
    private Animator anim;
    private Character_Controller characterContrillerScript;
    private Utils utilityScript;
    private float xDirection = 0;
    private bool onGround = false;
    private bool isFacingRight = false;
    private float flashTime = .5f;

    // Use this for initialization
    void Start()
    {
        playerInstance = GameObject.Find("Player");
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timer = JumpWaitTimer;
        playerScript = playerInstance.GetComponent<Player>();
        characterContrillerScript = playerInstance.GetComponent<Character_Controller>();
        utilityScript = GetComponent<Utils>();
    }

    // Update is called once per frame
    void FixedUpdate()
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

                //check if within attack distance

                Jump();
            }
        }

        //check for dead
        if (currentHitPoints < 0)
        {
            Debug.Log("i'm dead jim.");
            //playerInstance.GetComponent<Player>().score += scoreValue;
            //this will add HP to the player when an enemy is killed.
            playerScript.hitPoints += scoreValue;
            Destroy(gameObject);
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
        if (useConstantAttackAnimation)
        {
            anim.SetTrigger("Attack");
        }
        //check distance so not always firing attack animation.
        else if (Vector3.Distance(transform.position, playerInstance.transform.position) < attackDx)
        {
            anim.SetTrigger("Attack");
        }

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
         //transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
         transform.Rotate(Vector3.up, 180);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //hacky should do this better, probably use a ground tag
        if (col.gameObject.name == characterContrillerScript.nameOfBackgroundObject)
        {
            onGround = true;
        }
    }

    //fired when hit box collides
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            if (playerScript.IsHitBox(col))
            {
                //Debug.Log("Player hit me.");
                currentHitPoints -= playerScript.currentDamage;
                Debug.Log("flash");
                StartCoroutine(utilityScript.Flash(flashTime));
            }
        }
    }
}
