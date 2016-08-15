using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    private float shipY = 0;
    private float shipZ = 0;
    public float speed = 10f;
    private float xmin;
    private float xmax;
    private float padding = 1f;

    public GameObject projectile;
    public float projectile_speed = 10f;
    public float firing_rate = 1f;

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

        // if spacebar pressed, spawn projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firing_rate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    void Fire()
    {
        GameObject fired_projectile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        fired_projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectile_speed, 0);
    }
}
