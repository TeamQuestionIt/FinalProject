using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
   
    public Image healthBar;
    private float filled;
    private float maxHP;
    private Player playerScript;

	// Use this for initialization
	void Start () 
    {
        playerScript = GetComponent<Player>();
        maxHP = playerScript.hitPoints;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnGUI() 
    {
    
    }
}
