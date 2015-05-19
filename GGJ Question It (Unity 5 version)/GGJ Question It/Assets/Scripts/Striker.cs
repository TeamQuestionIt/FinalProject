using UnityEngine;
using System.Collections;

public class Striker : MonoBehaviour {

    public float strength = 40f;
    public float leftOffset = .8f;
    public float cooldown = .4f;

    private bool struck = false; //only hit once
    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        if(!FindObjectOfType<InputHandler>().gameObject.GetComponent<CharacterController>().facingRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            Vector2 newPos = transform.position;
            newPos.x -= leftOffset; //yay magic numbers!
            transform.position = newPos;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (struck) return;
        
        if(other.gameObject.layer == 11) //enemies
        {
            other.gameObject.GetComponent<MeleeMonster>().TakeDamage(strength);
            struck = true;
        }
    }

    public void KillSelf()
    {
        Destroy(gameObject.GetComponentInParent<Transform>().gameObject);
    }
}
