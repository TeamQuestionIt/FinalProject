using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour {

    public Image healthBar;
  
    private ImpAI impAIScript;
    
	
	

	
	// Use this for initialization
	void Start () 
	{
        impAIScript = GetComponent<ImpAI>();
	}
	
	// Update is called once per frame
	void Update () {
		


      
	}

    private void OnGUI()
    {
        if (healthBar != null )
        {

            // transform.position = Camera.main.WorldToScreenPoint(monsterPos.transform.position);
            healthBar.fillAmount = (float)impAIScript.currentHitPoints / impAIScript.hitPoints;


        }
    } 
}
