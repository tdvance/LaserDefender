using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
    private bool autoPlay = false;
    private Ball ball;
    private LevelManager levelManager;

    // Use this for initialization
    void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        autoPlay = LevelManager.autoPlay;
	}
	
	// Update is called once per frame
	void Update () {
        if (autoPlay)
        {
            MoveWithBall();
        }
        else
        {
            MoveWithMouse();
        }
	}

    void MoveWithBall()
    {
        this.transform.position = new Vector3(Mathf.Clamp(ball.transform.position.x, 0.75f, 15.25f), this.transform.position.y, this.transform.position.z);
    }

    void MoveWithMouse()
    {
        float mousePosInBlocks;

        mousePosInBlocks = Input.mousePosition.x / Screen.width * 16.0f;
        this.transform.position = new Vector3(Mathf.Clamp(mousePosInBlocks, 0.75f, 15.25f), this.transform.position.y, this.transform.position.z);
    }
}
