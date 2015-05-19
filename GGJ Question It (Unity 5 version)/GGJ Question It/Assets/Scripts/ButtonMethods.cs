using UnityEngine;
using System.Collections;

public class ButtonMethods : MonoBehaviour {

    public void StartGame()
    {
        Application.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
