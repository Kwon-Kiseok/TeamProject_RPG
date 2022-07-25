using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = bgm;
        audioSource.volume = 1.0f;
        audioSource.loop = true;
        audioSource.mute = false;

        audioSource.Play();
        audioSource.Stop();

        audioSource.playOnAwake = true;

        audioSource.priority = 0;
    }


}
