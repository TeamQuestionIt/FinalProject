using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    //this fixes compile bug in BodySwitcher.cs
    public GameObject attackPrefab;
    //velocity of repulsion force if player is hit.
    public Vector2 hitRepelVelocity = new Vector2(10, 100);
    public uint hitPoints = 100;
    //time between ability to power attack
    public float PowerMoveWaitTime = 3.0f;
    public BoxCollider2D[] hitBoxes;
    public BoxCollider2D currentHitBox;

    private Animator anim;
    private Rigidbody2D rBody;

    private bool isAttacking = false;
    private float timer = 0f;
    private bool canPowerMove = true;
    

    /// <summary>
    /// Type of attacks.
    /// </summary>
    public enum ATTACK
    {
        LIGHT,
        HEAVY,
        POWER
    }

    /// <summary>
    /// Used to set the bool isAttacking field by a string. 
    /// </summary>
    /// <param name="value">Either "true" or "false"</param>
    public void SetAttacking(string value)
    {
        isAttacking = Boolean.Parse(value);
    }

    /// <summary>
    /// Perform attack
    /// </summary>
    /// <param name="type">Type of attack to perform.</param>
    public void Attack(ATTACK type)
    {
        if (!isAttacking)
        {
            switch (type)
            {
                case ATTACK.LIGHT:
                    anim.SetTrigger("LightAttack");
                    SetHitBox(ATTACK.LIGHT);
                    isAttacking = true;
                    break;
                case ATTACK.HEAVY:
                    break;
                case ATTACK.POWER:
                    if (canPowerMove)
                    {
                        anim.SetTrigger("PowerAttack");
                        isAttacking = true;
                        canPowerMove = false;
                    }
                    break;
            }
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //can change this to timed coroutine if perf needed
        anim.SetFloat("Speed", Mathf.Abs(rBody.velocity.x));

        //only need to handle timer after power attack ran, so save some cycles
        if (!canPowerMove && timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
        else
        {
            timer = PowerMoveWaitTime;
            canPowerMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //check if enemy
        if (col.tag == "enemy")
        {
            //Debug.Log("enemy collided with my trigger.");

            //will need to make this more generic when more enemies added to family.
            //should make base class
            //check if hitbox
            if (col.gameObject.GetComponent<ImpAttack>().IsHitBox(col))
            {
                //repel
                //Debug.Log("Repel!");
                Rigidbody2D enemyRBody = col.GetComponent<Rigidbody2D>();

                //get repel direction
                int playerRepelDirection = 0;
                if (col.transform.position.x < transform.position.x)
                {
                    playerRepelDirection = 1;
                }
                else
                {
                    playerRepelDirection = -1;
                }



                //stop all velocity first
                rBody.velocity = Vector2.zero;
                enemyRBody.velocity = Vector2.zero;
                //playerRBody.AddForce(-playerRBody.velocity, ForceMode2D.Impulse);
                // enemyRBody.AddForce(-enemyRBody.velocity, ForceMode2D.Impulse);

                enemyRBody.velocity = new Vector2(hitRepelVelocity.x * -playerRepelDirection, hitRepelVelocity.y);
                rBody.velocity = new Vector2(hitRepelVelocity.x * playerRepelDirection, hitRepelVelocity.y);
                //enemyRBody.AddForce(new Vector2(hitRepelForce.x * -playerRepelDirection, hitRepelForce.y));
                //playerRBody.AddForce(new Vector2(hitRepelForce.x * playerRepelDirection, hitRepelForce.y));


                //take off hitpoints if any
                hitPoints -= (uint)col.gameObject.GetComponent<ImpAttack>().Damage;
                Debug.Log("Hitpoints: " + hitPoints);

            }

        }
    }

    private void SetHitBox(ATTACK attackType)
    {
        currentHitBox.size = hitBoxes[(int)attackType].size;
        currentHitBox.offset = hitBoxes[(int)attackType].offset;
    }

    public void ClearHitBox()
    {
        currentHitBox.size = Vector2.zero;
    }
}
