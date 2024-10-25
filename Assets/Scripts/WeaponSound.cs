using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _swingSound;
    [SerializeField] private AudioClip[] _hitSound;

    private void Start()
    {
        _audioSource = GetComponentInParent<AudioSource>();
    }

    public void PlaySwingSound()
    {
        _audioSource.PlayOneShot(_swingSound[Random.Range(0, _swingSound.Length-1)]);
        Debug.Log("Played In WeaponSound");
    }

    public void PlayHitSound()
    {
        _audioSource.PlayOneShot(_hitSound[Random.Range(0, _hitSound.Length - 1)]);
    }


}
