using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour {
    public Image healthBar;
    public Image specialBar;
    public Text livesText;

    private Player playerScript;private LifeManager lifeManagerScript;
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
 

    //private IEnumerator Flash()
    //{

    //    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
	
    //        //renderer.color = Color.Lerp(Color.red, Color.green, );
			
    //        /*if (currentStep > 1)
    //        {
    //            direction = -1;
    //        }
    //        else if (currentStep < 0)
    //        {
    //            direction = 1;
    //        }
    //        currentStep = currentStep + (step * direction);
    //        timer += Time.deltaTime;
    //        // yield return new WaitForSeconds(.1f);
    //        yield return null;*/
	
    //    renderer.color = Color.white;
    //    yield return null;
    //}



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

    }


}
