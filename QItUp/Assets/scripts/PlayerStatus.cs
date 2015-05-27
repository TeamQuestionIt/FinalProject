using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour {
    public Image healthBar;
    public Image specialBar;
    private Player playerScript;
	// Use this for initialization
	void Start () 
    {
        playerScript = GetComponent<Player>();
       

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
 
    private void OnGUI()
    { 
    if (healthBar!= null)
    {
        healthBar.fillAmount = (float)playerScript.hitPoints/ playerScript.maxHitPoints;

    }
    
    if (specialBar != null)
    {
        specialBar.fillAmount = (float)playerScript.PowerMoveTimeWaited /playerScript.PowerMoveWaitTime ;
        if(playerScript.canPowerMove)
        {
            specialBar.fillAmount = 1;

        }
    }

    }


}
