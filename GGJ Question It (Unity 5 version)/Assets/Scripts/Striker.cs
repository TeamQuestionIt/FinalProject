using UnityEngine;
using System.Collections;

public class Striker : MonoBehaviour {

    public float strength = 40f;
    public float leftOffset = .8f;
    public float cooldown = .4f;

    private bool alreadyStruck = false; //only hit once

    public void Start()
    {
        if(!FindObjectOfType<InputHandler>().gameObject.GetComponent<Character_Controller>().facingRight)
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
        if (alreadyStruck) return;
        
        if(other.gameObject.layer == 11) //enemies
        {
            other.gameObject.GetComponent<MeleeMonster>().TakeDamage(strength);
            alreadyStruck = true;
        }
    }

    public void KillSelf()
    {
        Destroy(transform.parent.gameObject);
    }
}
