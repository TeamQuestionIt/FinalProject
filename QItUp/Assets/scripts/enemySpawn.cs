using UnityEngine;
using System.Collections;

public class enemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float maxTimeBetween;
    public bool useRandomTimer = true;

    private float timer = 0;
    private float currentTimeBetween = 0;
    void Start()
    {
        currentTimeBetween = maxTimeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < currentTimeBetween)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Spawn();
            if (useRandomTimer)
            {
                currentTimeBetween = Random.Range(0, maxTimeBetween);
            }

            timer = 0;
        }
    }

    public void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
