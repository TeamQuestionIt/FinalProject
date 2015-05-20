using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {

    public GameObject folowedItem;
    //public float folowedXMin = 0;
    //public float folowedXMax = 0;

    //public float folowedYMin = 0;
    //public float folowedYMax = 0;
    public bool playerOOBClamp = true;

    public float cameraXMin = 0;
    public float cameraXMax = 1;
    public bool lockX = false;

    public float cameraYMin = 0;
    public float cameraYMax = 0;
    public bool lockY = true;

    public float panSpeed = 0;// 0 is instant

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float newX = transform.position.x;
        float newY = transform.position.y;
        if (!lockX)
        {
            //find where the camera needs to pan to
            newX = Lerp(cameraXMin, cameraXMax, LerpFindPlayerXT());
        }

        //clamp values if nessisary
        if (playerOOBClamp)
        {
            if (newX > cameraXMax)
            {
                newX = cameraXMax;
            } 
            else if (newX < cameraYMin)
            {
                newX = cameraXMin;
            }
        }

        transform.position = new Vector3(newX, newY, transform.position.z);
	
	}

    private float LerpFindPlayerXT()
    {
        float currentNumber = folowedItem.transform.position.x - cameraXMin;

        return currentNumber / cameraXMax;
    }

    private void LerpFindPlayerYT()
    {

    }

    private float Lerp(float v0, float v1, float t)
    {
        return (1 - t) * v0 + t * v1;
    }
}
