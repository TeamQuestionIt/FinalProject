using UnityEngine;
using System.Collections;

public class YRandomizer : MonoBehaviour {

    public float max = 1f;

	void Start () 
    {
        Vector2 newPos = transform.position;
        newPos.y += Random.value * (max - (max / 2));
        transform.position = newPos;
	}
}
