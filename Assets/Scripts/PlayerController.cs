using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private float shipY = 0;
    private float shipZ = 0;
    public float speed = 10f;
    private float xmin;
    private float xmax;
    private float padding = 1f;

	// Use this for initialization
	void Start () {
        //for movement---y and z stay the same.
        shipY = gameObject.transform.position.y;
        shipZ = gameObject.transform.position.z;
        xmin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, shipZ - Camera.main.transform.position.z)).x + padding;
        xmax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, shipZ - Camera.main.transform.position.z)).x - padding;
    }

    // Update is called once per frame
    void Update () {

        //Move the ship left or right in response to left-arrow or right-arrow keypress
        float x = gameObject.transform.position.x;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = Mathf.Clamp(x - speed*Time.deltaTime, xmin, xmax);
            gameObject.transform.position = new Vector3(x, shipY, shipZ);
        }else if (Input.GetKey(KeyCode.RightArrow))
        {
            x = Mathf.Clamp(x + speed * Time.deltaTime, xmin, xmax);
            gameObject.transform.position = new Vector3(x, shipY, shipZ);
        }
    }
}
