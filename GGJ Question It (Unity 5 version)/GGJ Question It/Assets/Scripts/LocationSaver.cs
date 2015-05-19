using UnityEngine;
using System.Collections;

public class LocationSaver : MonoBehaviour {

    private static int lastScene, sceneBeforeLast;
    private static Vector3 lastPosition, positionBeforeLast;
	
	void Start () 
    {
        if (sceneBeforeLast == Application.loadedLevel)
        {
            FindObjectOfType<InputHandler>().transform.position = positionBeforeLast;
        }
        sceneBeforeLast = lastScene;
        lastScene = Application.loadedLevel;
	}

    public static void saveLocation()
    {
        positionBeforeLast = lastPosition;
        lastPosition = FindObjectOfType<InputHandler>().transform.position;
    }
}
