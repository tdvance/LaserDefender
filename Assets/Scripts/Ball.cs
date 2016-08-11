using UnityEngine;
using System.Collections;
using System;

public class Ball : MonoBehaviour {
    private Paddle paddle;
    public bool isStarted = false;
    public LevelManager levelManager;
    private Vector3 paddleToBallVector;
    private float time;

    private static float min_velocity2 = 5.5f*5.5f;
    private static float max_velocity2 = 14.0f*14.0f;
    private static float min_slope = 0.5f;
    private static float max_slope = 4.0f;

	// Use this for initialization
	void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        time = 0f;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (isStarted)
        {
            GetComponent<AudioSource>().Play();
            Rigidbody2D body = this.GetComponent<Rigidbody2D>();

            body.velocity = adjust_velocity(body.velocity);
            Debug.Log("Velocity: " + Mathf.Sqrt(body.velocity.x * body.velocity.x + body.velocity.y * body.velocity.y));
         
            if (Brick.breakableCount <= 0)
            {
                levelManager.LoadNextLevel();
            }
        }
    }

    Vector2 adjust_velocity(Vector2 velocity)
    {
        float x = velocity.x;
        float y = velocity.y;
        x += UnityEngine.Random.Range(0f, 0.05f);
        y += UnityEngine.Random.Range(0f, 0.05f);

        if (x == 0)
        {
            x = 0.1f;
        }
        if (y == 0)
        {
            y = 0.1f;
        }
        float slope = y / x;
        if (slope > max_slope)
        {
            x *= slope / max_slope;
        }
        else if(-slope > max_slope)
        {
            x *= -slope / max_slope;
        }else if(-min_slope < slope && slope < min_slope)
        {
            x *= Mathf.Abs(slope) / min_slope;
        }
        float d = x * x + y * y;
        if(d < min_velocity2)
        {
            float f = Mathf.Sqrt(min_velocity2 / d);
            x *= f;
            y *= f;
        }else if(d > max_velocity2)
        {
           float f = Mathf.Sqrt(max_velocity2 / d);
            x *= f;
            y *= f;
        }
        
        return new Vector2(x, y);
    }


    // Update is called once per frame
    void Update () {
        if (!isStarted)
        {
            this.transform.position = paddle.transform.position + paddleToBallVector;

            if (Input.GetMouseButtonDown(0))
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 7.5f);
                isStarted = true;
            }
            else if (LevelManager.autoPlay)
            {
                //Debug.Log("Time: " + time);
                time += Time.deltaTime;
                if (time > 2.0f)
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 7.5f);
                    isStarted = true;
                }
            }
        }
	}
}
