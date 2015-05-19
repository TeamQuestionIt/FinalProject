using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private bool paused = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SetPause(!paused);
    }

    public void EndGame()
    {
        SetPause(true);
    }

    private void SetPause(bool p)
    {
        if (p != paused)
        {
            paused = p;
            if (p)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
}
