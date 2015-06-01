using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    private Character_Controller controller;
    //private PlayerAttack attack;
    private Player player;
    private float horizontalInput = 0f;

    public void Start()
    {
        controller = GetComponent<Character_Controller>();
        //attack = GetComponent<PlayerAttack>();
        player = GetComponent<Player>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            controller.Jump();
            return;
        }
            

        if (Input.GetButtonDown("Fire1"))
        {
            //attack.Attack(PlayerAttack.ATTACK.LIGHT);
            player.Attack(Player.ATTACK.LIGHT);
            return;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            //attack.Attack(PlayerAttack.ATTACK.POWER);
            player.Attack(Player.ATTACK.HEAVY);
            return;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            //attack.Attack(PlayerAttack.ATTACK.POWER);
            player.Attack(Player.ATTACK.POWER);
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
