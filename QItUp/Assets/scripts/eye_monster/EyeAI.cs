using UnityEngine;
using System.Collections;

public class EyeAI : MonoBehaviour
{
    public GameObject player;
    public Vector2 maxMoveSpeed;
    public Vector2 moveForce;
    public float attackMaxDistance;
    public int damage;
    
    public bool InAttackRange { get; private set; }

    private Vector2 direction = new Vector2(-1f, 0f);
    private float flashTime = .5f;
    private Rigidbody2D rBody;
    private Animator anim;
    public Player playerScript;


    void Start()
    {
        CheckAttackRange();
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerScript = player.GetComponent<Player>();
    }

    private void Update()
    {
        if (rBody.velocity.x != 0)
        {
            anim.SetFloat("xVelocity", Mathf.Abs(rBody.velocity.x));
        }
    }

    void FixedUpdate()
    {
        Move();
        CheckAttackRange();
    }

    private void CheckAttackRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackMaxDistance)
        {
            InAttackRange = true;
        }
        else
        {
            InAttackRange = false;
        }
    }

    private void Move()
    {
        if (!InAttackRange)
        {
            rBody.velocity = new Vector2(maxMoveSpeed.x * direction.x, maxMoveSpeed.y * direction.y);
        }
        else
        {
            var heading = (player.transform.position - transform.position).normalized;
            //equv: heading.x != direction.x FLOATS!!
            if(playerScript.OnGround && Mathf.Abs(heading.x - direction.x) > .1)
            {
                FlipDirection();
            }
            rBody.velocity = new Vector2(maxMoveSpeed.x * heading.x, 0);

        }

    }

    private void FlipDirection()
    {
        direction.x *= -1;
        transform.Rotate(Vector3.up, 180);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "walls")
        {
            FlipDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "walls")
        {
            FlipDirection();
        }
    }
}
