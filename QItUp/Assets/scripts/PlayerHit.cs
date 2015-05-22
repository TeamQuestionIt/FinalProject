using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour
{

    public Vector2 hitRepelVelocity = new Vector2(10,100);

    private Animator anim;
    private Rigidbody2D rBody;

    public void Hit(Collider2D hittingCollider)
    {
        Debug.Log(hittingCollider.gameObject.name + " hit me!");
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", rBody.velocity.x);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //check if enemy
        if (col.tag == "enemy")
        {
            Debug.Log("enemy collided with my trigger.");
            //check if hitbox
            if (col.gameObject.GetComponent<ImpAttack>().IsHitBox(col))
            {
                //repel
                Debug.Log("Repel!");
                Rigidbody2D playerRBody = GetComponent<Rigidbody2D>();
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
                playerRBody.velocity = Vector2.zero;
                enemyRBody.velocity = Vector2.zero;
                //playerRBody.AddForce(-playerRBody.velocity, ForceMode2D.Impulse);
                // enemyRBody.AddForce(-enemyRBody.velocity, ForceMode2D.Impulse);

                enemyRBody.velocity = new Vector2(hitRepelVelocity.x * -playerRepelDirection, hitRepelVelocity.y);
                playerRBody.velocity = new Vector2(hitRepelVelocity.x * playerRepelDirection, hitRepelVelocity.y);
                //enemyRBody.AddForce(new Vector2(hitRepelForce.x * -playerRepelDirection, hitRepelForce.y));
                //playerRBody.AddForce(new Vector2(hitRepelForce.x * playerRepelDirection, hitRepelForce.y));
                
            }

        }

    }
}
