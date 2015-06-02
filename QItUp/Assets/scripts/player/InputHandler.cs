using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    private Character_Controller controller;
    //private PlayerAttack attack;
    private Player playerScript;
    private float horizontalInput = 0f;

    public void Start()
    {
        controller = GetComponent<Character_Controller>();
        //attack = GetComponent<PlayerAttack>();
        playerScript = GetComponent<Player>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetAxis("Jump") > .1)
        {
            controller.Jump();
            return;
        }
            

        if (Input.GetButtonDown("Fire1"))
        {
            //attack.Attack(PlayerAttack.ATTACK.LIGHT);
            playerScript.Attack(Player.ATTACK.LIGHT);
            return;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            //attack.Attack(PlayerAttack.ATTACK.POWER);
            playerScript.Attack(Player.ATTACK.HEAVY);
            return;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            //attack.Attack(PlayerAttack.ATTACK.POWER);
            playerScript.Attack(Player.ATTACK.POWER);
            return;
        }

        if (Input.GetButtonDown("Pause"))
        {
            Application.LoadLevelAdditive("Final_pauseMenu");
        }

        //using buttons like this (left and right assigned in the input manager) gives more precise controls
        horizontalInput = Input.GetAxis("Horizontal");
        controller.Move(horizontalInput);
    }
}
