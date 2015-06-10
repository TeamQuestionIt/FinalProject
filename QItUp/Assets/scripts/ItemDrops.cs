using UnityEngine;
using System.Collections;

public class ItemDrops : MonoBehaviour {


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
            var playerStatus = col.GetComponent<PlayerStatus>();
            Player player = col.GetComponent<Player>();
            Debug.Log(playerStatus);
            Player.hitPoints += 20;
            Destroy(gameObject);
        }
    }
}
