using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 9f;
    public float height = 5f;
    private float formationY = 0;
    private float formationZ = 0;
    public float speed = 5f;
    private float xmin;
    private float xmax;
    private float dx;
    public float spawndelay=0.5f;

    // Use this for initialization
    void Start () {
        //y and z of formation for motion
        formationY = gameObject.transform.position.y;
        formationZ = gameObject.transform.position.z;

        //compute extents of horizontal motion
        xmin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, formationZ - Camera.main.transform.position.z)).x + width/2;
        xmax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, formationZ - Camera.main.transform.position.z)).x - width/2;

        //start motion rightward at set speed.
        dx = speed;

        //spawn the enemies at the selected positions (children of transform) in the formation.
        SpawnUntilFull();

    }

    public void spawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    public void SpawnUntilFull()
    {
        Transform pos = NextFreePosition();
        if (pos)
        {
            GameObject enemy = Instantiate(enemyPrefab, pos.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = pos;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawndelay);
        }
    }

    public void OnDrawGizmos()
    {
        //allow editor to show box illustrating transform
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {
        //move the ship horizontally, bouncing at the screen extents
        float x = gameObject.transform.position.x;
        x = Mathf.Clamp(x + dx * Time.deltaTime, xmin, xmax);
        if(x >= xmax)
        {
            dx = -speed;
        }else if(x<= xmin)
        {
            dx = speed;
        }
        gameObject.transform.position = new Vector3(x, formationY, formationZ);

        if (AllMembersDead())
        {
            //respawn a new set of enemies
            SpawnUntilFull();
        }

    }

    private Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    private bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
