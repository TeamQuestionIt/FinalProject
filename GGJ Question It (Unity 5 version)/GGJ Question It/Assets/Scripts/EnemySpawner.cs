using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject Player;
    public GameObject enemyPrefab;
    public float delay = 3f; //seconds between each spawn

    private float timer = 0f;


    void Start()
    {
        timer = delay;
    }
	
	void Update () 
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = delay;

            GameObject newMonster = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
            newMonster.GetComponent<MeleeMonster>().SetTarget(Player);
        }
	}
}
