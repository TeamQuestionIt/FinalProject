using UnityEngine;
using System.Collections;
using System;

public class PlayerAttack : MonoBehaviour
{
    //left over from previous code base
    public GameObject attackPrefab;

    //time between ability to power attack
    public float PowerMoveWaitTime = 3.0f;
   
    private bool isAttacking = false;
    private float timer = 0f;
    private bool canPowerMove = true;
    private Animator anim;

    /// <summary>
    /// Type of attacks.
    /// </summary>
    public enum ATTACK
    {
        LIGHT,
        HEAVY,
        POWER
    }

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        //only need to handle timer after power attack ran, so save some cycles
        if (!canPowerMove && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = PowerMoveWaitTime;
            canPowerMove = true;
        }

    }

    /// <summary>
    /// Used to set the bool isAttacking field by a string. 
    /// </summary>
    /// <param name="value">Either "true" or "false"</param>
    public void SetAttacking(string value)
    {
        isAttacking = Boolean.Parse(value);
    }

    public void Attack(ATTACK type)
    {
        bool isIdleState = anim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        if (!isAttacking)
        {
            switch (type)
            {
                case ATTACK.LIGHT:
                    anim.SetTrigger("LightAttack");
                    isAttacking = true;
                    break;
                case ATTACK.HEAVY:
                    break;
                case ATTACK.POWER:
                    if(canPowerMove)
                    {
                        anim.SetTrigger("PowerAttack");
                        isAttacking = true;
                        canPowerMove = false;
                    }
                    break;
            }
        }
    }
}
