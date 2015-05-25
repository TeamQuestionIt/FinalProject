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
    public int currentDamage = 0;
    //time between ability to power attack
    public float PowerMoveWaitTime = 3.0f;
    public BoxCollider2D[] hitBoxes;
    public BoxCollider2D currentHitBox;
    public Text scoreUI;
    public int score = 0;


    private Animator anim;
    private Rigidbody2D rBody;

    private bool isAttacking = false;
    private float timer = 0f;
    private bool canPowerMove = true;
    private int[] damage;
    private string scoreLabel = "Score: ";
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

    private void OnGUI()
    {
        scoreUI.text = scoreLabel + score.ToString("D5");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //check if enemy
        if (col.gameObject.tag == "enemy")
        {

            //Debug.Log("enemy collided with my trigger.");


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




            if (!isAttacking)
            {
                //take off hitpoints if any
                hitPoints -= col.gameObject.gameObject.GetComponent<ImpAttack>().Damage;
                //Debug.Log("Hitpoints: " + hitPoints);
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
        while(timer < flashTime)
        {
            Debug.Log("loop");
            renderer.color = Color.Lerp(Color.red, Color.green, currentStep);
           
            if(currentStep > 1)
            {
                direction = -1;
            }
            else if(currentStep < 0)
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

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    //check if enemy
    //    if (col.tag == "enemy")
    //    {
    //        //Debug.Log("enemy collided with my trigger.");

    //        //will need to make this more generic when more enemies added to family.
    //        //should make base class
    //        //check if hitbox
    //        if (col.gameObject.GetComponent<ImpAttack>().IsHitBox(col))
    //        {
    //            //repel
    //            //Debug.Log("Repel!");
    //            Rigidbody2D enemyRBody = col.GetComponent<Rigidbody2D>();

    //            //get repel direction
    //            int playerRepelDirection = 0;
    //            if (col.transform.position.x < transform.position.x)
    //            {
    //                playerRepelDirection = 1;
    //            }
    //            else
    //            {
    //                playerRepelDirection = -1;
    //            }



    //            //stop all velocity first
    //            rBody.velocity = Vector2.zero;
    //            enemyRBody.velocity = Vector2.zero;

    //            //repel them
    //            //make sure velocity is not over max (bug fix)
    //            enemyRBody.velocity = new Vector2(Mathf.Clamp(hitRepelVelocity.x * -playerRepelDirection, 0, maxRepelVelocity.x), Mathf.Clamp(hitRepelVelocity.y, 0, maxRepelVelocity.y));
    //            rBody.velocity = new Vector2(Mathf.Clamp(hitRepelVelocity.x * playerRepelDirection, 0, maxRepelVelocity.x), Mathf.Clamp(hitRepelVelocity.y, 0, maxRepelVelocity.y));




    //            //take off hitpoints if any
    //            hitPoints -= col.gameObject.GetComponent<ImpAttack>().Damage;
    //            //Debug.Log("Hitpoints: " + hitPoints);

    //        }

    //    }
    //}

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
