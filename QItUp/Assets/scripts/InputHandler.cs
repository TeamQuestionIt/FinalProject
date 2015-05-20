using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    private Character_Controller controller;
    private PlayerAttack attack;

    public void Start()
    {
        controller = GetComponent<Character_Controller>();
        attack = GetComponent<PlayerAttack>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
            controller.Jump();

        if (Input.GetButtonDown("Fire1"))
            attack.StartAttack();

        //using the axis this way left a kind of slow-down time to stopping that was difficult to control
        /*float horiz = Input.GetAxis("Horizontal");
        controller.SetDirection(new Vector2(horiz, 0f));*/

        //using buttons like this (left and right assigned in the input manager) gives more precise controls
        float horizontalInput = Input.GetAxis("Horizontal");
        controller.SetDirection(new Vector2(horizontalInput, 0f));
    }

    public void FixedUpdate()
    {
        
    }
}
