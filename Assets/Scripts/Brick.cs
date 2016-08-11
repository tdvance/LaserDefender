using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    public Sprite[] hitSprites;

    public AudioClip brickBroke;
    public static int breakableCount = 0;
    private int numHits = 0;
    private LevelManager levelManager;
    private bool isBreakable;
    public GameObject smoke;


    // Use this for initialization
    void Start () {
        numHits = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        isBreakable = (this.tag == "Breakable");
        if (isBreakable)
        {
            breakableCount++;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("breakableCount: " + breakableCount);
        //GameObject[] breakable = GameObject.FindGameObjectsWithTag("Breakable");
        //Debug.Log("actual breakableCount: " + breakable.Length);
       

        if (isBreakable) {
            AudioSource.PlayClipAtPoint(brickBroke, transform.position);
            HandleHits();
        }else
        {
            LevelManager.score += 15;
        }
    }

    void HandleHits()
    {
        numHits++;
        if (numHits > hitSprites.Length)
        {
            breakableCount--;
            GameObject puff = Instantiate(smoke, this.transform.position, Quaternion.identity) as GameObject;
            puff.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
            LevelManager.score += 100;
            //levelManager.BrickDestroyed();
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
    }

    void LoadSprites()
    {
        int spriteIndex = numHits - 1;
        if (hitSprites[spriteIndex])
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }else
        {
            Debug.LogError("Sprite missing, index: " + spriteIndex);
        }
    }
    
}
