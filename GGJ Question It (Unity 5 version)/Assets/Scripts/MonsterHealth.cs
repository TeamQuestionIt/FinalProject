using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {

	public GameObject healthBar;
	public GameObject monster;
	public Transform monsterPos;
	public Vector3 healthPos;

	
	// Use this for initialization
	void Start () 
	{


	}
	
	// Update is called once per frame
	void Update () {
		if (healthBar != null && monsterPos != null) 
		{
			healthPos = monsterPos.position;
			healthPos.y += monsterPos.GetComponent<SpriteRenderer> ().bounds.size.y / 2f;
			healthPos.x += monsterPos.GetComponent<SpriteRenderer> ().bounds.size.x / 12f;
			transform.position = Camera.main.WorldToScreenPoint (healthPos);
			healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (monster.GetComponent<HealthTracker> ().maxHealth, 20f);
		}

		if (monsterPos == null)
			Destroy (healthBar);


	}
}
