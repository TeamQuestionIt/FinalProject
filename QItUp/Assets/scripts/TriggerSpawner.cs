using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class TriggerSpawner : MonoBehaviour {

	// Use this for initialization
    public GameObject monster0;
    public GameObject monster1;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
                enabled = true;
        }
        if (col.name == "Player" && monster0 != null)
        {
            monster0.SetActive(true);
        }
        if (col.name == "Player" && monster1 != null)
        {
            monster1.SetActive(true);
        }



    }
}
