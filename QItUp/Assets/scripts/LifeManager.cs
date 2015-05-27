using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{

    public int LivesLeft { get; set; }
    public int totalLives;

    // Use this for initialization
    void Start()
    {
        LivesLeft = totalLives - 1;
    }

    public void Die()
    {
        Debug.Log("need to implement player die.");
    }
}
