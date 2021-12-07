using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioSource backgroundMusicSource;
    private void Awake()
    {
        instance = this;
    }

    public void PlayHitSound()
    {
        soundEffectSource.clip = hitSound;
        soundEffectSource.Play();
    }

    public void TurnOnOffBackgroundMusic(bool IsOff)
    {
        backgroundMusicSource.mute = IsOff;
    }
    public void TurnOnOffSoundEffect(bool IsOff)
    {
        soundEffectSource.mute = IsOff;
    }
}
