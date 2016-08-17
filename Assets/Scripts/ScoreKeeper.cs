using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreKeeper : MonoBehaviour {
    public int score = 0;
    public Text text;

    public static int global_score;
    public static int highScore;
    public float highScoreInterval = 10f;
    private float highScoreTime = 0f;

    private static string save_filename;

    // Use this for initializatino
    void Start () {
         save_filename= Application.persistentDataPath + "/laser_defender_hs.gd";
        loadHighScore();
        Reset();
	}

    public void Score(int points)
    {
        score += points;
        text.text = "Score: " + score;
        global_score = score;
        if(score > highScore)
        {
            highScore = score;
            saveHighScorePeriodically();
        }
    }


    public static void SaveHighScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("Saving high score:" + highScore + " to: " + save_filename);
        FileStream file = File.Open(save_filename, FileMode.OpenOrCreate);
        bf.Serialize(file, highScore);
        file.Close();
    }
    private void saveHighScorePeriodically()
    {
        if(highScoreTime <= 0)
        {
            SaveHighScore();
            highScoreTime = highScoreInterval;
        }else
        {
            highScoreTime -= Time.deltaTime;
        }
    }

    private void loadHighScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(save_filename))
        {
            FileStream infile = File.Open(save_filename, FileMode.Open);
            int hs = (int)bf.Deserialize(infile);
            infile.Close();
            if(hs > highScore)
            {
                highScore = hs;
            }
            Debug.Log("Loading high score:" + highScore + " from: " + save_filename);
        }else
        {
            highScore = 4050;
        }
    }

    public void Reset()
    {
        score = 0;
        text.text = "Score: " + score;
        global_score = score;
    }
    
}
