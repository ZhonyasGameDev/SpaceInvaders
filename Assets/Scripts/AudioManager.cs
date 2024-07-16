using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // public static AudioManager instance;

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private Player player;
    [SerializeField] private MysteryShip mysteryShip;
    public AudioSource audioSource;
    public AudioSource mysteryShipSFX;
    public AudioClip backgroundMusic;
    private void Start()
    {
        PlayMusic(backgroundMusic);

        player.OnShooting += Player_OnShooting;
        GameManager.Instance.OnInvaderKilledEvent += Invader_OnInvaderKilled;
        GameManager.Instance.OnPlayerKilledEvent += GameManager_OnPlayerKilled;
        mysteryShip.OnSpawn += MysteryShip_OnSpawn;
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
    // Play a sound effect
    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlayMysteryShipSFX(AudioClip clip)
    {
        mysteryShipSFX.clip = clip;
        mysteryShipSFX.loop = true;
        mysteryShipSFX.Play();
    }

    private void Player_OnShooting()
    {
        PlaySFX(audioClipRefsSO.PLAYER_FIRE);
    }
    private void Invader_OnInvaderKilled()
    {
        PlaySFX(audioClipRefsSO.INVADER_KILLED);
    }
    private void GameManager_OnPlayerKilled()
    {
        PlaySFX(audioClipRefsSO.PLAYER_EXPLOSION);
        // StopSound(audioSource);
    }
    private void MysteryShip_OnSpawn(bool spawned)
    {
        if (spawned)
        {
            PlayMysteryShipSFX(audioClipRefsSO.MYSTERY_SHIP);
        }
        else
        {
            // StopSFX();
            StopSound(mysteryShipSFX);
        }
    }

    // Stop the music
    public void StopSound(AudioSource audioSource)
    {
        // musicSource.Stop();
        audioSource.Stop();
        // audioSource.Pause();
    }

    /*   public void StopSFX()
      {
          sfxSource.Stop();
      } */

}