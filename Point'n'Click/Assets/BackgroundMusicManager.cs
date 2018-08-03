using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour {
    
    private static AudioSource backgroundAudioSource;

    private void Awake()
    {
        backgroundAudioSource = GetComponent<AudioSource>();
    }

    public static void Play()
    {
        backgroundAudioSource.Play();
    }

    public static void Stop()
    {
        backgroundAudioSource.Stop();
    }
}
