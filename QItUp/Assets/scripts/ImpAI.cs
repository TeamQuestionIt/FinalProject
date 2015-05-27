using UnityEngine;
using System.Collections;

public class ImpAI : MonoBehaviour
{
    public Vector2 jumpForce = new Vector2(10f, 250f);
    public float attackDx = 10f;//maximum distance from player to start attack
    public float dxMultiplier = 10f;//needed if using variable jumping
    public Vector2 maxJumpVelocity = new Vector2(10f, 10f);
    public float JumpWaitTimer = 2f;
    public int hitPoints = 20;
    public int currentHitPoints = 20;
    public int scoreValue = 10;
    public uint damage = 5;
    public bool useVariableJumpDistance = true;
    public bool useVariableJumpTimer = true;
    public bool useConstantAttackAnimation = false;

    //could randomize this a bit 1-5?
    private float timer = 0;

    private GameObject playerInstance;
    private Rigidbody2D rBody;
    private Animator anim;
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
        if (hitPoints < 0)
        {
            Debug.Log("i'm dead jim.");
            playerInstance.GetComponent<Player>().score += scoreValue;
            Destroy(gameObject);
        }

        //debug
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
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
        Vector3 scale = transform.localScale;
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

    //fired when hit box collides
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            Player playerScript = col.gameObject.GetComponent<Player>();
            if (playerScript.IsHitBox(col))
            {
                //Debug.Log("Player hit me.");
                hitPoints -= playerScript.currentDamage;
                StartCoroutine("Flash");
            }
        }
    }

    private IEnumerator Flash()
    {
        float timer = 0;
        float step = .1f;
        float currentStep = 0;
        int direction = 1;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        while (timer < flashTime)
        {
            renderer.color = Color.Lerp(Color.red, Color.green, currentStep);

            if (currentStep > 1)
            {
                direction = -1;
            }
            else if (currentStep < 0)
            {
                direction = 1;
            }
            currentStep = currentStep + (step * direction);
            timer += Time.deltaTime;
            // yield return new WaitForSeconds(.1f);
            yield return null;
        }
        renderer.color = Color.white;
        yield return null;
    }
}
