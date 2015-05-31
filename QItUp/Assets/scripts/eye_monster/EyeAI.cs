using UnityEngine;
using System.Collections;

public class EyeAI : MonoBehaviour
{
    public GameObject player;
    public Vector2 moveSpeed;
    public float attackMaxDistance;
    public int damage;

    public bool InAttackRange { get; private set; }

    private Vector2 direction = new Vector2(-1, 0);
    private float flashTime = .5f;
    

    void Start()
    {
        CheckAttackRange();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckAttackRange();
        if(InAttackRange)
        {
            Debug.Log("Attack");
        }
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
        Vector3 translation = new Vector3(moveSpeed.x * direction.x, moveSpeed.y * direction.y, 0);
        transform.Translate(translation);
    }
}
