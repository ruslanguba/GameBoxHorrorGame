using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockedSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpenSound()
    {
        PlaySound(openSound);
    }

    public void PlayCloseSound()
    {
        PlaySound(closeSound);
    }

    public void PlayLockedSound()
    {
        PlaySound(lockedSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
