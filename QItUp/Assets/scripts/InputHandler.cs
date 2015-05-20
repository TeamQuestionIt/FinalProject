using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    private Character_Controller controller;
    private PlayerAttack attack;
    private float horizontalInput = 0f;

    public void Start()
    {
        controller = GetComponent<Character_Controller>();
        attack = GetComponent<PlayerAttack>();
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
            attack.Attack(PlayerAttack.ATTACK.LIGHT);
            return;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            attack.Attack(PlayerAttack.ATTACK.POWER);
            return;
        }

        //using buttons like this (left and right assigned in the input manager) gives more precise controls
        horizontalInput = Input.GetAxis("Horizontal");
        controller.Move(horizontalInput);
    }
}
