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
    

    void Start()
    {
        CheckAttackRange();
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
    }

    private void Move()
    {
        Vector3 translation = new Vector3(maxMoveSpeed.x * direction.x, maxMoveSpeed.y * direction.y, 0);
       // transform.Translate(translation);
        rBody.velocity = new Vector2(maxMoveSpeed.x * direction.x, maxMoveSpeed.y * direction.y);
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
