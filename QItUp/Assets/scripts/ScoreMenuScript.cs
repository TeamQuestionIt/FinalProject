using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMenuScript : MonoBehaviour
{
    public GameObject[] scoreTexts;

    // Use this for initialization
    void Start()
    {
        ScoreManager scoreManager = gameObject.GetComponent<ScoreManager>();
        if (!scoreManager.IsLoaded)
        {
            scoreManager.LoadHighscores();
        }


        List<int> highscores = scoreManager.GetHighScores();

        for(int i = 0; i < highscores.Count; i++)
        {
            scoreTexts[i].GetComponent<TextMesh>().text = highscores[i].ToString();
        }

    }
}
