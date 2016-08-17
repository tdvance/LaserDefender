using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    private static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip loseClip;

    private AudioSource music;

    // Use this for initialization
    void Awake () {
        if(instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            print("Duplicate music player self-destructed");
        }
	}
	
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
        music.clip = startClip;
        music.loop = true;
        music.Play();

    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("MusicPlayer: loaded level: " + level);
        AudioClip oldclip = music.clip;
        switch (level)
        {
            case 0:
            case 1:
                music.clip = startClip;
                break;
            case 2:
                music.clip = gameClip;
                break;
            default:
                music.clip = loseClip;
                break;
        }
        if (oldclip != music.clip)
        {
            music.Stop();
            music.loop = true;
            music.Play();
        }
    }
}
