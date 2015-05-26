using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {

	public GameObject healthBar;
	public GameObject monster;
	public Transform monsterPos;
	public Vector3 healthPos;
	public float xOffset;
	public float yOffset;

	
	// Use this for initialization
	void Start () 
	{


	}
	
	// Update is called once per frame
	void Update () {
		if (healthBar != null && monsterPos != null) 
		{
			healthPos = monsterPos.position;
			healthPos.x += xOffset;
			healthPos.y += yOffset;
			transform.position = Camera.main.WorldToScreenPoint (healthPos);
			healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (monster.GetComponent<HealthTracker> ().maxHealth, 20f);
		}

		if (monsterPos == null)
			Destroy (healthBar);


	}
}
