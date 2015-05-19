using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour {

    //public float strength = 10f;
    public LayerMask canHit;
    public Transform attackPoint;

	[FormerlySerializedAs("attackPrefab")]
    public GameObject lightAttackPrefab;
	public GameObject heavyAttackPrefab;
	public GameObject specialAttackPrefab;


    private float timer = 0f;
    private Animator anim;
    private bool attacking = false;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    public void LightAttack()
    {
		if(timer <= 0 && !attacking && lightAttackPrefab != null)
        {
        anim.SetTrigger("Attack");
		anim.SetTrigger("LightAttack");
		attacking = true;
        }
    }
	public void HeavyAttack()
	{
		if(timer <= 0 && !attacking && heavyAttackPrefab != null)
		{
			anim.SetTrigger("Attack");
			anim.SetTrigger("HeavyAttack");
			attacking = true;
		}
	} 
	public void SpecialAttack()
	{
		if(timer <= 0 && !attacking && specialAttackPrefab != null)
		{
			anim.SetTrigger("Attack");
			anim.SetTrigger("SpecialAttack");
			attacking = true;
		}
	}
	public void FireLightAttack()
	{
		GameObject atk = Instantiate(lightAttackPrefab, attackPoint.position, attackPoint.rotation) as GameObject;
		Physics2D.IgnoreCollision(atk.GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
		attacking = false;
		
		timer = .1f;
	}

	public void FireHeavyAttack()
	{
		GameObject atk = Instantiate(heavyAttackPrefab, attackPoint.position, attackPoint.rotation) as GameObject;
		Physics2D.IgnoreCollision(atk.GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
		attacking = false;
		
		timer = 1;
	}
	
	public void FireSpecialAttack()
    {
		GameObject atk = Instantiate(specialAttackPrefab, attackPoint.position, attackPoint.rotation) as GameObject;
        Physics2D.IgnoreCollision(atk.GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
		attacking = false;
	
		timer = 3;

    }


	/*
    public void Attack()
    {
        Collider2D other = Physics2D.OverlapCircle(attackPoint.position, .2f, canHit);

        if (other != null)
        {
            other.GetComponent<MeleeMonster>().TakeDamage(strength);

        }
    }*/



}



