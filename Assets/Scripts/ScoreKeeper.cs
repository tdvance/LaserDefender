using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    public int score = 0;

    public Text text;

	// Use this for initializatino
	void Start () {
        Reset();
	}

    public void Score(int points)
    {
        score += points;
        text.text = "Score: " + score;
    }

    public void Reset()
    {
        score = 0;
        text.text = "Score: " + score;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
