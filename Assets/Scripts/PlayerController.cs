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
    public float health = 250;
    public bool autoplay = false;

    private float dx = 1f;
    private bool firing = false;


    public GameObject projectile;
    public float projectile_speed = 10f;
    public float firing_rate = 1f;

    public AudioClip fire;
    public AudioClip die;


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
        if (autoplay)
        {
            moveRandomly();
            fireRandomly();

        }
        else
        {
            moveWithKeypresses();
            fireWithKeypress();
        }

    }
    
    void moveRandomly()
    {
        float x = gameObject.transform.position.x;
        if (UnityEngine.Random.value / Time.deltaTime < 1 || x==xmin || x==xmax)
        {
            dx = -dx;
        }
        x = Mathf.Clamp(x + dx* speed * Time.deltaTime, xmin, xmax);
        gameObject.transform.position = new Vector3(x, shipY, shipZ);
    }

    void fireRandomly()
    {
        if (UnityEngine.Random.value/ Time.deltaTime < 10f)
        {
            if (firing)
            {
                CancelInvoke("Fire");
                firing = false;
            }
            else
            {
                InvokeRepeating("Fire", 0.000001f, firing_rate);
                firing = true;
            }
        }
    }

    void moveWithKeypresses()
    {

        //Move the ship left or right in response to left-arrow or right-arrow keypress
        float x = gameObject.transform.position.x;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = Mathf.Clamp(x - speed * Time.deltaTime, xmin, xmax);
            gameObject.transform.position = new Vector3(x, shipY, shipZ);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            x = Mathf.Clamp(x + speed * Time.deltaTime, xmin, xmax);
            gameObject.transform.position = new Vector3(x, shipY, shipZ);
        }

    }

    void fireWithKeypress()
    {
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
        AudioSource.PlayClipAtPoint(fire, transform.position, 1.0f);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyProjectile projectile = collider.gameObject.GetComponent<EnemyProjectile>();
        if (projectile)
        {
            projectile.Hit();
            health -= projectile.GetDamage();
            if (health <= 0)
            {
                Die();
            }
        }
    }


    void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(die, transform.position, 1.0f);
    }

}
