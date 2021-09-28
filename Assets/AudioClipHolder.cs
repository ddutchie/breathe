using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipHolder : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;

    // Lazy Audio clip manager.
    public void PlayClip(int x)
    {
        audioSource.clip = clips[x];
        audioSource.Play();
    }

}
