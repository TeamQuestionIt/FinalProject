using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject Player;
    public GameObject enemyPrefab;
	public GameObject enemyHealth;
    public float delay = 3f; //seconds between each spawn
	public int spawnCount = 0;
    private float timer = 0f;



    void Start()
    {
        timer = delay;
    }
	
	void Update () 
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && spawnCount < 3)
        {
            timer = delay;
			spawnCount += 1;

            GameObject newMonster = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
            newMonster.GetComponent<MeleeMonster>().SetTarget(Player);

			Canvas healthBar = Instantiate(enemyHealth, transform.position, transform.rotation) as Canvas;
			healthBar.transform.position = Camera.main.WorldToScreenPoint(newMonster.transform.position);

        }



	}



}
