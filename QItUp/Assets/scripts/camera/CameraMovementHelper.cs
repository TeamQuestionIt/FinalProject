using UnityEngine;
using System.Collections;

public class CameraMovementHelper : MonoBehaviour {

    GameObject gameCamera;
    bool callOnStart = false;

    public float cameraScrollLeft = 0;
    public float cameraScrollRight = 0;
    public bool lockX = true;

    public float cameraScrollDown = 0;
    public float cameraScrollUp = 0;
    public bool lockY = true;

    public float newPanSpeedX = 0;// 0 is instant
    public float newPanSpeedY = 0;

	// Use this for initialization
	void Start () {
        gameCamera = Camera.main.gameObject;
        if (callOnStart)
        {
            MoveCameraHere();
        }
	}

    public void MoveCameraHere()
    {
        //move camera (long form 'cus i'm paranoid about reference semantics)
        gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        CameraHandler currentHandeler = gameCamera.GetComponent<CameraHandler>();

        currentHandeler.lockX = lockX;

        currentHandeler.cameraXMin = transform.position.x - cameraScrollLeft;
        currentHandeler.cameraXMax = transform.position.x + cameraScrollRight;

        currentHandeler.lockY = lockY;

        currentHandeler.cameraYMin = transform.position.y - cameraScrollDown;
        currentHandeler.cameraYMax = transform.position.y + cameraScrollUp;

        currentHandeler.panSpeedX = newPanSpeedX;
        currentHandeler.panSpeedY = newPanSpeedY;
    }
}
