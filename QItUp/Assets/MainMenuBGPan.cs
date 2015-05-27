using UnityEngine;
using System.Collections;

public class MainMenuBGPan : MonoBehaviour {

    public float startY = 6.5f;
    public float endY = -6.5f;

    public float speed = 0.11f;
    bool movingDown = true;
    public float stopTime = 5.0f;
    float stopingTime = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (stopingTime > 0)
        {
            stopingTime -= Time.deltaTime;
        } 
        else if (movingDown)
        {
            transform.Translate(0, -speed, 0);
            if (transform.position.y <= endY)
            {
                movingDown = false;
                stopingTime = stopTime;
            }
        }
        else
        {
            transform.Translate(0, speed, 0);
            if (transform.position.y >= startY)
            {
                movingDown = true;
                stopingTime = stopTime;
            }
        }
	}
}
