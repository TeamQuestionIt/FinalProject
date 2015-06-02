using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class TriggerSpawner : MonoBehaviour {

	// Use this for initialization
    public GameObject monster;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider2D col)
    {
        if (col.name == "Player")
        {
                enabled = true;
        }
    }
}
