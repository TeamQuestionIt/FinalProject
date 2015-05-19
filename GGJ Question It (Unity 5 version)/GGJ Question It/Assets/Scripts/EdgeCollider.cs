using UnityEngine;
using System.Collections;

public class EdgeCollider : MonoBehaviour {

    public string nextScene = "";

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9) //player
        {
            LocationSaver.saveLocation();
            Application.LoadLevel(nextScene);
        }
            
    }
}
