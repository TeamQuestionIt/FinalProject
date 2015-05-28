using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour {
    public Image healthBar;
    public Image specialBar;
    public Text livesText;

    private Player playerScript;
    private LifeManager lifeManagerScript;
	// Use this for initialization
	void Start () 
    {
        playerScript = GetComponent<Player>();
        lifeManagerScript = GetComponent<LifeManager>();

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

    livesText.text = lifeManagerScript.LivesLeft.ToString();

    }


}
