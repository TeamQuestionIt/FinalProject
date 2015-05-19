using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public float strength = 10f;
    public LayerMask canHit;
    public Transform attackPoint;
    public GameObject attackPrefab;

    private float timer = 0f;
    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    public void StartAttack()
    {
        if(timer <= 0 && attackPrefab != null)
        {
            anim.SetTrigger("Attack");
            GameObject atk = Instantiate(attackPrefab, attackPoint.position, attackPoint.rotation) as GameObject;
            timer = atk.GetComponentInChildren<Striker>().cooldown;
            Physics2D.IgnoreCollision(atk.GetComponentInChildren<Collider2D>(), collider2D);
        }
    }

    public void Attack()
    {
        Collider2D other = Physics2D.OverlapCircle(attackPoint.position, .2f, canHit);
        if (other != null)
        {
            other.GetComponent<MeleeMonster>().TakeDamage(strength);
        }
    }
}
