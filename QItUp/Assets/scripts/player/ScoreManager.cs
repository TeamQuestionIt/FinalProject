using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

    public static int Score { get; set; }
    public bool IsLoaded
    {
        get
        {
            if(highScores.Count > 0 || PlayerPrefs.GetInt("0") == 0)
            {
                return true;
            }
            return false;
        }
    }
    private static List<int> highScores = new List<int>();
    private const int HIGH_SCORE_COUNT = 3;

    public void AddScore(int amountToAdd)
    {
        Score += amountToAdd;
    }

    public List<int> GetHighScores()
    {
        return highScores;
    }

    public static void SaveHighScores()
    {
        //see if we need score added to list
        if (highScores.Count < HIGH_SCORE_COUNT)
        {
            highScores.Add(Score);
            highScores.Sort();
            highScores.Reverse();
        }
        else if(IsHighScore(Score))
        {
            //yes higher than one of them, add it sort it and reverse it
            highScores.Add(Score);
            highScores.Sort();
            highScores.Reverse();

            //remove the ones on bottom
            highScores.RemoveRange(HIGH_SCORE_COUNT, highScores.Count - HIGH_SCORE_COUNT);
        }
        //now add the qty of highscores to disk
        int count = 0;
        foreach(int score in highScores)
        {
            if(count < HIGH_SCORE_COUNT)
            {
                PlayerPrefs.SetInt(count.ToString(), score);
                count++;
            }
            else
            {
                //more than wanted qty so finished.
                return;
            }
            
        }

    }

    private static bool IsHighScore(int a_Score)
    {
        foreach(int highScore in highScores)
        {
            if(a_Score > highScore)
            {
                return true;
            }
        }
        return false;
    }

    public void LoadHighscores()
    {
        for(int i = 0; i < HIGH_SCORE_COUNT; i++)
        {
            highScores.Add(PlayerPrefs.GetInt(i.ToString()));
        }
    }

    public static void resetScore()
    {
        Score = 0;
    }

}
