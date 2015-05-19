using UnityEngine;
using System.Collections;

public class DoorCollider : MonoBehaviour
{
    public string nextScene = ""; 
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == 9 && Input.GetKey(KeyCode.R)) //player
        {
            Application.LoadLevel(nextScene);
        }
    }
}
