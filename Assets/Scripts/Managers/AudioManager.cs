using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    [SerializeField]
    private AudioClip bulletShotClip;
    [SerializeField]
    private AudioClip asteroidExplosionClip;
    [SerializeField]
    private AudioClip crashClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        EventsManager.Instance.AsteroidShotted += OnAsteroidShotted;
        EventsManager.Instance.BulletFired += OnBulletFired;
        EventsManager.Instance.PlayerLoseLife += PlayerLoseLife;
    }

    private void OnAsteroidShotted()
    {
        _audioSource.PlayOneShot(asteroidExplosionClip);
    }

    private void OnBulletFired()
    {
        _audioSource.PlayOneShot(bulletShotClip);
    }

    private void PlayerLoseLife(uint obj)
    {
        _audioSource.PlayOneShot(crashClip);
    }
}
