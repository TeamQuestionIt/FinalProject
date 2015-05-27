using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{

    public int LivesLeft { get; private set; }
    public int totalLives;

    // Use this for initialization
    void Start()
    {
        LivesLeft = totalLives - 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
