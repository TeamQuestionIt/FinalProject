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
            Application.LoadLevel("LevelTwo");
            Debug.Log("you ended the level");
        }

    }
}
