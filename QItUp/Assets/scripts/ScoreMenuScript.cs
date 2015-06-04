using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMenuScript : MonoBehaviour
{

    public GameObject ScoreText0;
    public GameObject ScoreText1;
    public GameObject ScoreText2;

    // Use this for initialization
    void Start()
    {
        ScoreManager scoreManager = gameObject.GetComponent<ScoreManager>();
        if (scoreManager.GetHighScores().Count == 0)
        {
            scoreManager.LoadHighscores();
        }


        List<int> highscores = scoreManager.GetHighScores();

        ScoreText0.GetComponent<TextMesh>().text = "0";
        ScoreText1.GetComponent<TextMesh>().text = "0";
        ScoreText1.GetComponent<TextMesh>().text = "0";

        if (highscores.Count >= 1)
        {
            ScoreText0.GetComponent<TextMesh>().text = highscores[0].ToString();

            if (highscores.Count >= 2)
            {
                ScoreText1.GetComponent<TextMesh>().text = highscores[1].ToString();

                if (highscores.Count >= 3)
                {
                    ScoreText2.GetComponent<TextMesh>().text = highscores[2].ToString();
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
