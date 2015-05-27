using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour {

	public Image healthBar;
	//public GameObject monster;
	//public Transform monsterPos;
    private ImpAI impAIScript;
	
	public float xOffset;
	public float yOffset;

	
	// Use this for initialization
    void Start()
    {
        impAIScript = GetComponent<ImpAI>();


    }
    void OnGUI()
    {
        healthBar.fillAmount = (float)impAIScript.currentHitpoints / (float)impAIScript.hitPoints;
    }
}
