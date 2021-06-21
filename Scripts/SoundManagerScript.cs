using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip webSound, jumpSound, killSound, metalSound;
    static AudioSource audioSrc;

    void Start()
    {
        webSound = Resources.Load<AudioClip>("web-shoot");
        jumpSound = Resources.Load<AudioClip>("jump");
        killSound = Resources.Load<AudioClip>("killsound");
        metalSound = Resources.Load<AudioClip>("metalhit");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlayWebSound()
    {
        audioSrc.PlayOneShot(webSound);
    }
    public static void PlayJumpSound()
    {
        audioSrc.PlayOneShot(jumpSound);
    }
    public static void PlayKillSound()
    {
        audioSrc.PlayOneShot(killSound);
    }
    public static void PlayMetalSound()
    {
        audioSrc.PlayOneShot(metalSound);
    }
}
