using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    private GameManager manager;
    private Character_Controller controller;
    public PlayerAttack attack;

    public void Start()
    {
        manager = FindObjectOfType<GameManager>();
        controller = GetComponent<Character_Controller>();
        attack = GetComponent<PlayerAttack>();
    }

    public void Update()
    {
        if (Input.GetAxis("Jump") > 0)
            controller.Jump();

		//THIS IS ALL THE STUFF THAT'S FUKT UP WITH ATTACKING
        if (Input.GetAxis ("Fire1") > 0)
			//attack.FireLightAttack();
			attack.LightAttack();
			

		if (Input.GetAxis ("Fire2") > 0)
			//attack.FireHeavyAttack();
			attack.HeavyAttack ();
			
			

		if (Input.GetAxis("Fire3") > 0)
			//attack.FireSpecialAttack();
			attack.SpecialAttack();
			//Fire special attack makes player shoot what's assigned to the prefab in player holder
		//special attack shoots whatever is assigned in light attack prefab for some reason


        //using the axis this way left a kind of slow-down time to stopping that was difficult to control
        /*float horiz = Input.GetAxis("Horizontal");
        controller.SetDirection(new Vector2(horiz, 0f));*/

        //using buttons like this (left and right assigned in the input manager) gives more precise controls
        float horizontalInput = 0;
        if (Input.GetButton("Left"))
            horizontalInput -= 1;
        if (Input.GetButton("Right"))
            horizontalInput += 1;
        controller.SetDirection(new Vector2(horizontalInput, 0f));
    }

    public void FixedUpdate()
    {
        
    }
}
