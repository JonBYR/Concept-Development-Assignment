using System;
using UnityEngine.Audio;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip death;
    public AudioClip shoot;
    public AudioClip hit;
    public AudioSource audioSource;
    public void PlayDeath()
    {
        audioSource.PlayOneShot(death);
    }
    public void PlayShoot()
    {
        audioSource.PlayOneShot(shoot);
    }
    public void PlayHit()
    {
        audioSource.PlayOneShot(hit);
    }
}
