using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    private static MusicPlayer instance = null;

	// Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            print("Duplicate music player self-destructed");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
