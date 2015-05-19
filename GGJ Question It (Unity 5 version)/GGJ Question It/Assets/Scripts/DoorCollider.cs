using UnityEngine;
using System.Collections;

public class DoorCollider : MonoBehaviour
{
    public string nextScene = "";

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 9 && Input.GetAxis("Jump") > 0) //player
        {
            LocationSaver.saveLocation();
            Application.LoadLevel(nextScene);
        }
    }
}
