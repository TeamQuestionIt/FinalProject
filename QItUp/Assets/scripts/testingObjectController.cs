using UnityEngine;
using System.Collections;

public class testingObjectController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0.11f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -0.11f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.11f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.11f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject.Find("Room1").GetComponent<CameraMovementHelper>().MoveCameraHere();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject.Find("Room2").GetComponent<CameraMovementHelper>().MoveCameraHere();
        }
	}
}
