﻿using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{

    public static int LivesLeft { get; set; }
    public int totalLives;
    public GameObject[] spawners;
    
    private int currentSpawner;

    // Use this for initialization
    void Start()
    {
        //LivesLeft = totalLives - 1;
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
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        Player.hitPoints = GetComponent<Player>().maxHitPoints;
    }

    public static void ResetLives()
    {
        LivesLeft = 3 - 1;//cant use totalLives because it is an instance variable
    }
}
