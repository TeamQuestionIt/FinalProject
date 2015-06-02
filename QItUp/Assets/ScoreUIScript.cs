using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUIScript : MonoBehaviour {
    public Text something;
    
	// Use this for initialization
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void OnGUI () 
    {
        something.text = "Score:" + ScoreManager.Score;
	}
}
