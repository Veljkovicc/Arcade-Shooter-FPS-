using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    public AudioSource ShootingChannel;
    public AudioClip shootingSound;
    public AudioSource reloadingSound;

    public AudioSource playerChannel;
    public AudioClip playerHurt1;
    public AudioClip playerHurt2;
    public AudioClip playerDeath;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
