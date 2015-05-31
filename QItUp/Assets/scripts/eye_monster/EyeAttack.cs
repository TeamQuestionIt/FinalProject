using UnityEngine;
using System.Collections;

public class EyeAttack : MonoBehaviour {

    public float strikeRange;

    private EyeAI aIScript;
    private bool isAttacking;

	// Use this for initialization
	void Start () {
        aIScript = GetComponent<EyeAI>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
