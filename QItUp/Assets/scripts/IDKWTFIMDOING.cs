using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
        GetComponent<Text>().text = "Score: " + ScoreManager.Score;

	}
}
