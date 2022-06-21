using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Player Sound")]
    public AudioClip swipeSound;
    public AudioClip shootSound;
    public AudioClip playerDamageSound;
    public AudioClip goldSound;
    public AudioClip bulletSound;
    public AudioClip buttonSound;
    public AudioClip slowMotionSound;
    public AudioClip healSound;
    public AudioClip[] scoreDamageSound;
    public AudioClip[] fuelClickSound;
    public AudioClip[] slimeBulletSound;
    public AudioClip boxDropClickSound;
    public AudioClip boxDropCrackSound;
    public AudioClip buyButtonSound;
    public AudioClip woodSound;
    public AudioClip expSound;
    public AudioClip bossShoot;
    public AudioClip emptyBullet;

    public static SoundManager Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundHit(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
