using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurpManager : MonoBehaviour {

    private static AudioSource blurpAudioSource;

    private void Awake()
    {
        blurpAudioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization

    public static void Play()
    {
        blurpAudioSource.Play();
    }

    public static void Stop()
    {
        blurpAudioSource.Stop();
    }
}
