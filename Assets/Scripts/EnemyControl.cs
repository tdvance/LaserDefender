using UnityEngine;
using System.Collections;
using System;

public class EnemyControl : MonoBehaviour {

    public float health = 250;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
