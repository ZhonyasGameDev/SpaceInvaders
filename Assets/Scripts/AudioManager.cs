using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // public static AudioManager instance;

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private Player player;
    [SerializeField] private MysteryShip mysteryShip;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    // public List<AudioClip> soundEffects;


    /*  private void Awake()
     {
         // Singleton pattern to ensure only one instance of AudioManager exists
         if (instance == null)
         {
             instance = this;
             DontDestroyOnLoad(gameObject);
         }
         else
         {
             Destroy(gameObject);
         }
     }
  */
    private void Start()
    {
        PlayMusic(backgroundMusic);

        player.OnShooting += Player_OnShooting;
        GameManager.Instance.OnInvaderKilledEvent += Invader_OnInvaderKilled;
        GameManager.Instance.OnPlayerKilledEvent += GameManager_OnPlayerKilled;
        mysteryShip.OnSpawn += MysteryShip_OnSpawn;
    }

    // Play background music
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayMysteryShipSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.loop = true;
        sfxSource.Play();
    }

    // Play a sound effect
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Play a sound effect by name
    /*  public void PlaySFX(string name)
     {
         AudioClip clip = soundEffects.Find(x => x.name == name);
         if (clip != null)
         {
             PlaySFX(clip);
         }
         else
         {
             Debug.LogWarning("Sound effect not found: " + name);
         }
     } */

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
            StopSound(sfxSource);
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