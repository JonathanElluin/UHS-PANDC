using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonflementManager : MonoBehaviour {

    private static AudioSource ronflementAudioSource;

    private void Awake()
    {
        ronflementAudioSource = this.GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Play()
    {
        ronflementAudioSource.Play();
    }

    public static void Stop()
    {
        ronflementAudioSource.Stop();
    }

    public static void StopLoop()
    {
        ronflementAudioSource.loop = false;
    }
}
