using UnityEngine;
using System.Collections;
using System;

public class EnemyControl : MonoBehaviour {

    public float health = 250;
    public GameObject projectile;
    public float projectile_speed = 10f;
    public float firing_rate = 1f;
    private float fire_time = 0f;
    private PlayerController playerController;

    // Use this for initialization
    void Start () {
        playerController = FindObjectOfType<PlayerController>();

    }
	
	// Update is called once per frame
	void Update () {
        fire_time += Time.deltaTime;
        if(UnityEngine.Random.value < 0.5f && playerController)
        {
            float diff = Mathf.Abs(transform.position.x - playerController.transform.position.x);
            if (UnityEngine.Random.value < 0.5f)
            {
                if(diff < 0.25)
                {
                    Fire();
                }
            }else
            {
                float dist = Mathf.Abs(transform.position.y - playerController.transform.position.y);
                if (Mathf.Abs(dist - diff) < 0.25)
                {
                    Fire();
                
                }
            }
        }
	}

    void Fire()
    {
        if (fire_time > firing_rate)
        {
            //Debug.Log("Fire!");
            GameObject fired_projectile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            fired_projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectile_speed, 0);
            fire_time = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile projectile = collider.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            projectile.Hit();
            health -= projectile.GetDamage();
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    
}
