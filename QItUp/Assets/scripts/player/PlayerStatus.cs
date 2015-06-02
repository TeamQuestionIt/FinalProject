using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour {
    public Image healthBar;
    public Image specialBar;
    public Text livesText;
    public Text score;
    private Player playerScript;
    private LifeManager lifeManagerScript;
    private Utils utilityScript;
	// Use this for initialization
	void Start () 
    {
        playerScript = GetComponent<Player>();
        lifeManagerScript = GetComponent<LifeManager>();
        utilityScript = GetComponent<Utils>();

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
 
    private void OnGUI()
    { 
    if (healthBar!= null)
    {
        healthBar.fillAmount = (float)Player.hitPoints/ playerScript.maxHitPoints;

    }
    
    if (specialBar != null)
    {
        specialBar.fillAmount = (float)playerScript.PowerMoveTimeWaited /playerScript.PowerMoveWaitTime ;
        specialBar.color = Color.yellow;
        //must be yellow by default or it will be green
        if(playerScript.canPowerMove)
        {
            specialBar.fillAmount = 1;
            specialBar.color = Color.green;
        }
    }

    livesText.text = LifeManager.LivesLeft.ToString();
    score.text = playerScript.score.ToString();
    }


}
