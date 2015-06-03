using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EyeAttack : MonoBehaviour
{
    public float strikeRange;
    public int hitPoints;
    public int currentHitPoints;
    public GameObject playerInstance;
    public BoxCollider2D[] hitBoxes;
    public BoxCollider2D currentHitBox;
    public BoxCollider2D hittableBox;
    public Canvas healthBarCanvas;
    private EyeAI aIScript;
    private bool isAttacking = false;
    private Animator anim;
   private ScoreManager scoreManagerScript;



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
        scoreManagerScript = playerInstance.GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            //bug fix #4
            if (col.IsTouching(hittableBox) && aIScript.playerScript.IsHitBox(col))
            {
                ApplyDamage();
            }
            else
            {
                Attack();
            }

        }
    }
    private void Attack()
    {
        anim.SetTrigger("attack");
    }

    private void ApplyDamage()
    {
        //bug fix #4
        currentHitPoints -= aIScript.playerScript.currentDamage;
        if (currentHitPoints <= 0)
        {
            KillHealthBar();
            //BUGBUG//if(anim.GetCurrentAnimationClipState(0))
            anim.SetTrigger("die");
        }
    }

    private void KillHealthBar()
    {
        foreach(var obj in healthBarCanvas.GetComponentsInChildren<Image>())
        {
            Destroy(obj);
        }
    }

    //called when die animation complete
    public void Kill()
    {
        scoreManagerScript.AddScore(10);
        Destroy(gameObject);
    }


}
