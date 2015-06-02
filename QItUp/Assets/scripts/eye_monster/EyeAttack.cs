﻿using UnityEngine;
using System.Collections;

public class EyeAttack : MonoBehaviour
{
    public GameObject player;
    public float strikeRange;
    public int hitPoints;
    public int currentHitPoints;
    public BoxCollider2D[] hitBoxes;
    public BoxCollider2D currentHitBox;
    private EyeAI aIScript;
    private bool isAttacking = false;
    private Animator anim;
    private SoundManager soundManagerScript;



    public void SetHitbox(int hitboxIndex)
    {
        BoxCollider2D box = hitBoxes[hitboxIndex];
        currentHitBox.offset = box.offset;
        currentHitBox.size = box.size;
    }

    public void ClearHitbox()
    {
        currentHitBox.offset = Vector2.zero;
        currentHitBox.size = Vector2.zero;
    }

    public bool IsHitBox(Collider2D col)
    {
        BoxCollider2D box = col as BoxCollider2D;
        if (null == box) return false;

        foreach (BoxCollider2D hitBox in hitBoxes)
        {
            if (hitBox.offset == box.offset && hitBox.size == box.size)
            {
                return true;
            }
        }
        return false;
    }

    // Use this for initialization
    void Start()
    {
        aIScript = GetComponent<EyeAI>();
        anim = GetComponent<Animator>();
        soundManagerScript = player.GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            if (aIScript.playerScript.IsHitBox(col))
            {
                ApplyDamage();
            }
            else
            {
                Attack();
                soundManagerScript.Play(SoundManager.Clip.Eye_Attack);
            }

        }


    }

    private void Attack()
    {
        anim.SetTrigger("attack");
    }

    private void ApplyDamage()
    {
        hitPoints -= aIScript.playerScript.currentDamage;
        if (hitPoints < 0)
        {
            anim.SetTrigger("die");
        }
    }

    //called when die animation complete
    public void Kill()
    {
        Destroy(gameObject);
    }


}
