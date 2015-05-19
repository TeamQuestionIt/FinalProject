using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    private GameManager manager;
    private CharacterController controller;
    private PlayerAttack attack;

    public void Start()
    {
        manager = FindObjectOfType<GameManager>();
        controller = GetComponent<CharacterController>();
        attack = GetComponent<PlayerAttack>();
    }

    public void Update()
    {
        if (Input.GetAxis("Jump") > 0)
            controller.Jump();

        if (Input.GetAxis("Fire1") > 0)
            attack.StartAttack();
    }

    public void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        controller.SetDirection(new Vector2(horiz, 0f));
    }
}
