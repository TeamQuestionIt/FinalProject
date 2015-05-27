﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour {

    public Image healthBar;
    public float filled;
    public float maxHP;
    public ImpAI impAIScript;
    
	
	//public float xOffset;
	//public float yOffset;

	
	// Use this for initialization
	void Start () 
	{
        impAIScript = GetComponent<ImpAI>();
        maxHP = impAIScript.hitPoints;
	}
	
	// Update is called once per frame
	void Update () {
		


      
	}

    private void OnGUI()
    {
        if (healthBar != null )
        {

            // transform.position = Camera.main.WorldToScreenPoint(monsterPos.transform.position);
            healthBar.fillAmount = impAIScript.hitPoints / maxHP;


        }

        if (transform.position == null)
            Destroy(healthBar);
    } 
}
