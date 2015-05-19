using UnityEngine;
using System.Collections;
using System;

public class MeleeMonster : MonoBehaviour {

    public GameObject player;
    public float reach = .5f; //how close the monster will attack
    public float attackStrength = 10f;
    public Transform attackPoint;
    public LayerMask canHit;
    public float cooldown = 1f;

    private float timer = 0f;

    private Vector2 move = new Vector2();
    private Character_Controller controller;
    private Animator anim;
    private HealthTracker health;

    public void Start()
    {
        controller = GetComponent<Character_Controller>();
        anim = GetComponent<Animator>();
        health = GetComponent<HealthTracker>();
    }
    
    public void Update()
    {
        if (health.dead)
        {
            move = new Vector2();
            return;
        }

        if (timer > 0)
            timer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > reach && Math.Abs(transform.position.x-player.transform.position.x) > .01f) //player too far, walk closer
        {
            move = new Vector2(1f, 0f);
            if (transform.position.x > player.transform.position.x)
                move.x *= -1;
        }
        else //close enough, attack!
        {
            move = new Vector2(); //stand still to attack

            if (Math.Abs(transform.position.y - player.transform.position.y) > 1) return; //player is over or under monster, don't swing
            
            if (timer <= 0)
            {
                timer = cooldown;
                anim.SetTrigger("Attack");
            }
        }

        if (player.GetComponent<BodySwitcher>().currentBody == BodySwitcher.bodyType.lamp)
        {
            
            move = new Vector2(-1f, 0f);
            if (transform.position.x > player.transform.position.x)
                move.x *= -1;
        }

        controller.SetDirection(move);
    }

     private void Attack()
     {
         if (health.dead) return;

         Collider2D target = Physics2D.OverlapCircle(attackPoint.position, .2f, canHit);
         if (target != null)
         {
             target.GetComponent<HealthTracker>().dealDamage(attackStrength);   
         }
     }

     public void TakeDamage(float dmg)
     {
         if (health.dealDamage(dmg))
         {
             controller.SetDirection(new Vector2());
             anim.SetTrigger("Dead");
             GetComponent<Collider2D>().enabled = false;
         }
     }

     public void KillSelf()
     {
         Destroy(gameObject);
     }

     public void SetTarget(GameObject target)
     {
         player = target;
     }
}