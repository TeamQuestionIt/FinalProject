using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    //this fixes compile bug in BodySwitcher.cs
    public GameObject attackPrefab;
    //velocity of repulsion force if player is hit.
    public Vector2 hitRepelVelocity = new Vector2(3, 5);
    public Vector2 maxRepelVelocity = new Vector2(5, 10);
    public int hitPoints = 100;
    public int maxHitPoints = 100;
    public int currentDamage = 0;
    //time between ability to power attack
    public float PowerMoveWaitTime = 3.0f;
    public float PowerMoveTimeWaited = 3.0f;
    public BoxCollider2D[] hitBoxes;
    public BoxCollider2D currentHitBox;

    public bool OnGround
    {
        get
        {
            return charControllerScript.onGround;
        }
    }


    private Animator anim;
    private Rigidbody2D rBody;
    private LifeManager lifeManagerScript;
    private ScoreManager scoreManagerScript;
    private Character_Controller charControllerScript;
    private Utils utilityScript;
    

    private bool isAttacking = false;
    public float timer = 0f;
    public bool canPowerMove = true;
    private int[] damage;
    private float flashTime = .5f;

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
                    SetHitBox(type);//move out of switch when all implemented
                    isAttacking = true;
                    break;
                case ATTACK.HEAVY:
                    break;
                case ATTACK.POWER:
                    if (canPowerMove)
                    {
                        anim.SetTrigger("PowerAttack");
                        SetHitBox(type);
                        isAttacking = true;
                        canPowerMove = false;
                    }
                    break;
            }
            currentDamage = damage[(int)type];
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        damage = new int[] { 10, 25, 100 };
        lifeManagerScript = GetComponent<LifeManager>();
        scoreManagerScript = GetComponent<ScoreManager>();
        charControllerScript = GetComponent<Character_Controller>();
        utilityScript = GetComponent<Utils>();
    }

    //debug
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            lifeManagerScript.Die();
        }
    }

    private void FixedUpdate()
    {
        PowerMoveTimeWaited = PowerMoveWaitTime - timer;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        //check if enemy
        if (col.gameObject.tag == "enemy")
        {
            Rigidbody2D enemyRBody = col.gameObject.GetComponent<Rigidbody2D>();

            //get repel direction
            int playerRepelDirection = 0;
            if (col.gameObject.transform.position.x < transform.position.x)
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

            //repel them
            //make sure velocity is not over max (bug fix)

            enemyRBody.velocity = new Vector2(Mathf.Clamp(hitRepelVelocity.x * -playerRepelDirection, -maxRepelVelocity.x, maxRepelVelocity.x), Mathf.Clamp(hitRepelVelocity.y, -maxRepelVelocity.y, maxRepelVelocity.y));
            rBody.velocity = new Vector2(Mathf.Clamp(hitRepelVelocity.x * playerRepelDirection, -maxRepelVelocity.x, maxRepelVelocity.x), Mathf.Clamp(hitRepelVelocity.y, -maxRepelVelocity.y, maxRepelVelocity.y));
            //enemyRBody.velocity = new Vector2(Mathf.Clamp(hitRepelVelocity.x * -playerRepelDirection, -maxRepelVelocity.x, maxRepelVelocity.x), 0);
            //rBody.velocity = new Vector2(Mathf.Clamp(hitRepelVelocity.x * playerRepelDirection, -maxRepelVelocity.x, maxRepelVelocity.x), 0);

            //check if hit from behind
            bool HitFromBehind = false;
            if (col.transform.position.x > transform.position.x && !charControllerScript.isFacingRight)
            {
                HitFromBehind = true;
            }
            else if (col.transform.position.x < transform.position.x && charControllerScript.isFacingRight)
            {
                HitFromBehind = true;
            }

            if (!isAttacking || HitFromBehind)
            {
                ApplyDamage(col.collider);

            }
        }
    }

    private void ApplyDamage(Collider2D col)
    {
        //get appropriate script from enemy type
        ImpAI impAiScript = col.GetComponent<ImpAI>();
        EyeAI eyeAIScript = col.gameObject.GetComponentInParent<EyeAI>();

        //get the damage
        int damage = 0;

        if (null != impAiScript)
        {
            damage = impAiScript.damage;
        }
        else if (null != eyeAIScript)
        {
            damage = eyeAIScript.damage;
        }

        hitPoints -= damage;
        if (hitPoints > 0)
        {
            StartCoroutine(utilityScript.Flash(flashTime));
        }
        else if (lifeManagerScript.LivesLeft > 1)
        {
            //dead with lives left
            lifeManagerScript.LivesLeft--;
            hitPoints = 100;
            gameObject.SetActive(false);
            lifeManagerScript.Die();
        }
        else
        {
            //game over
            Debug.Log("implement game over");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "hitBox")
        {
            //get repel direction
            int playerRepelDirection = 0;
            if (col.gameObject.transform.position.x < transform.position.x)
            {
                playerRepelDirection = 1;
            }
            else
            {
                playerRepelDirection = -1;
            }
            rBody.velocity = new Vector2(Mathf.Clamp(hitRepelVelocity.x * playerRepelDirection, -maxRepelVelocity.x, maxRepelVelocity.x), Mathf.Clamp(hitRepelVelocity.y, -maxRepelVelocity.y, maxRepelVelocity.y));


            ApplyDamage(col);
        }
    }

    //this sets the hitbox to the appropriate size and offset, the animation will clear it when done.
    private void SetHitBox(ATTACK attackType)
    {
        currentHitBox.size = hitBoxes[(int)attackType].size;
        currentHitBox.offset = hitBoxes[(int)attackType].offset;
    }

    //sets the hitbox size to zero making it inoperable
    //usually called by animation
    public void ClearHitBox()
    {
        currentHitBox.size = Vector2.zero;
    }

    //returns if given collider is the hitbox used by other objects to see if they were hit by hitbox
    public bool IsHitBox(Collider2D col)
    {
        BoxCollider2D box = col as BoxCollider2D;
        if (null == box)
        {
            return false;
        }
        return box.size == currentHitBox.size && box.offset == currentHitBox.offset;
    }
}
