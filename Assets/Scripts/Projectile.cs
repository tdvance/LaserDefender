using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour {
    public float damage = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Hit()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
