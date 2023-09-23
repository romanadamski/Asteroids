using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    [SerializeField]
    private AudioClip bulletShotClip;
    [SerializeField]
    private AudioClip explosionClip;
    [SerializeField]
    private AudioClip crashClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        EventsManager.Instance.AsteroidShotted += AsteroidShotted;
        EventsManager.Instance.EnemyShotted += AsteroidShotted;
        EventsManager.Instance.BulletFired += BulletFired;
        EventsManager.Instance.PlayerLoseLife += PlayerLoseLife;
    }

    private void AsteroidShotted(string tag)
    {
        _audioSource.PlayOneShot(explosionClip);
    }
    
    private void BulletFired()
    {
        _audioSource.PlayOneShot(bulletShotClip);
    }
    //todo some sound on player lose life
    private void PlayerLoseLife(uint lives)
    {
        //_audioSource.PlayOneShot(crashClip);
    }
}
