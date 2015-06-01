using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{

    public int LivesLeft { get; set; }
    public int totalLives;
    public GameObject[] spawners;

    private int currentSpawner;

    // Use this for initialization
    void Start()
    {
        LivesLeft = totalLives - 1;
        currentSpawner = 0;
    }

    public void Die()
    {
        //check if not last spawner
        if (currentSpawner != spawners.Length - 1)
        {
            //check if need to increment current
            if (transform.position.x > spawners[currentSpawner + 1].transform.position.x)
            {
                currentSpawner++;
            }
        }
        Spawn();
    }

    private void Spawn()
    {
        transform.position = spawners[currentSpawner].transform.position;
        gameObject.SetActive(true);
    }
}
