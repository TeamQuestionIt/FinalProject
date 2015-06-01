using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour {
    public Image healthBar;
    public Image specialBar;
    private Player playerScript;
	private float flashTime = 5f;
	// Use this for initialization
	void Start () 
    {
        playerScript = GetComponent<Player>();
       

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
 

	private IEnumerator Flash()
	{

		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
	
			renderer.color = Color.Lerp(Color.red, Color.green, );
			
			/*if (currentStep > 1)
			{
				direction = -1;
			}
			else if (currentStep < 0)
			{
				direction = 1;
			}
			currentStep = currentStep + (step * direction);
			timer += Time.deltaTime;
			// yield return new WaitForSeconds(.1f);
			yield return null;*/
	
		renderer.color = Color.white;
		yield return null;
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
				//Flash();
				specialBar.color.g = 100f;
        }
    }


    }


}
