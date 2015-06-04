using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
    
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            //only call on end of game
            //ScoreManager.SaveHighScores();
            //no next level to load (shoulden't be hardcoded anyway)
            // Application.LoadLevel("LevelTwo");
            Application.LoadLevel("FINAL_mainMenu");
            
            Debug.Log("you ended the level");
        }

    }
}
