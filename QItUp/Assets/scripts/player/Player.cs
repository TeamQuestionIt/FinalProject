﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    //STATIC
    public static int hitPoints = 300;

    //this fixes compile bug in BodySwitcher.cs
    public GameObject attackPrefab;
    //velocity of repulsion force if player is hit.
    public Vector2 hitRepelVelocity = new Vector2(3, 5);
    public Vector2 maxRepelVelocity = new Vector2(5, 10);
    
    public int maxHitPoints;
    public int currentDamage;
    //time between ability to power attack
    public float PowerMoveWaitTime = 3.0f;
    public float PowerMoveCurrentWaitTime = 3.0f;
    public BoxCollider2D[] hitBoxes;
    public BoxCollider2D currentHitBox;
    public SpriteRenderer gateSwitch;
    public int score = 0;
    public int[] damage;

    public bool CanWalk { get; set; }

    public bool OnGround
    {
        get
        {
            return charControllerScript.onGround;
        }
        set
        {
            charControllerScript.onGround = value;
        }
    }


    private Animator anim;
    private Rigidbody2D rBody;
    private LifeManager lifeManagerScript;
    private ScoreManager scoreManagerScript;
    private Character_Controller charControllerScript;
    private Utils utilityScript;
    private SoundManager soundManagerScript;
    

    private bool isAttacking = false;
    private float timer = 0f;
    public bool canPowerMove = true;
    //private int[] damage;
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
                    SetHitBox(type);
                    soundManagerScript.Play(SoundManager.Clip.TV_Light_Attack_Hit);
                    break;
                case ATTACK.HEAVY:
                    anim.SetTrigger("HeavyAttack");
                    SetHitBox(type);
                    soundManagerScript.Play(SoundManager.Clip.TV_Light_Attack_Hit);
                    break;
                case ATTACK.POWER:
                    if (canPowerMove)
                    {
                        anim.SetTrigger("PowerAttack");
                        soundManagerScript.Play(SoundManager.Clip.TV_Power_Attack);
                        SetHitBox(type);
                        canPowerMove = false;
                    }
                    break;
            }
            currentDamage = damage[(int)type];
            isAttacking = true;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        lifeManagerScript = GetComponent<LifeManager>();
        scoreManagerScript = GetComponent<ScoreManager>();
        charControllerScript = GetComponent<Character_Controller>();
        utilityScript = GetComponent<Utils>();
        soundManagerScript = GetComponent<SoundManager>();

        CanWalk = true;

        //debug REMOVE FOR RELEASE
        LifeManager.ResetLives();
    }
    //debug
    private void Update()
    {
        if(!isAttacking && currentHitBox.size != Vector2.zero)
        {
            ClearHitBox();
        }
    }

    private void FixedUpdate()
    {
        PowerMoveCurrentWaitTime = PowerMoveWaitTime - timer;
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

        //fix for # 17
        if (hitPoints > 300)
        {
            ResetHealth();
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
           //setting onground to false prevents jumping. fixes bug #2
            OnGround = false;
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
                soundManagerScript.Play(SoundManager.Clip.Ouch);

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
        else if (LifeManager.LivesLeft > 1)
        {
            //dead with lives left
            LifeManager.LivesLeft--;
            hitPoints = 100;
            gameObject.SetActive(false);
            lifeManagerScript.Die();
        }
        else
        {
            //game over
            Application.LoadLevel("FINAL_mainMenu");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //eye mopnster only one to hit with dynamic boxes (trigger) so assume it's eye monster and check if hitbox
        if(!isAttacking && col.name == "hitBox")
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
            //fix for bug #5
            OnGround = false;
            rBody.velocity = new Vector2(Mathf.Clamp(hitRepelVelocity.x * playerRepelDirection, -maxRepelVelocity.x, maxRepelVelocity.x), Mathf.Clamp(hitRepelVelocity.y, -maxRepelVelocity.y, maxRepelVelocity.y));

            soundManagerScript.Play(SoundManager.Clip.Ouch);
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

    public static void ResetHealth()
    {
        hitPoints = 300;//cant be maxHitpoints because maxhitpoints is an instance variable an this function is static
    }
}
