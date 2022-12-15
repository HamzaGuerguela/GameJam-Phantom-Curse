using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{

    #region Inspector

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
    [SerializeField] private Sound[] sounds;
    
    
    [SerializeField] private AudioClip[] stepSounds;

    [SerializeField] private AudioSource footSteps;
    
    
    [SerializeField] private AudioClip[] jumpSounds;
    
    [SerializeField] private AudioSource jumpSoundAudioSource;
    
    
    [SerializeField] private AudioClip[] deathSounds;
    
    [SerializeField] private AudioSource deathSoundAudioSource;
    
    
    [SerializeField] private AudioClip[] SwordSounds;
    
    [SerializeField] private AudioSource swordSoundAudioSource;

    
    [SerializeField] private AudioClip[] CheckpointSounds;
    
    [SerializeField] private AudioSource checkpointSoundAudioSource;
    
    
    [SerializeField] private AudioClip[] enemySounds;
    
    [SerializeField] private AudioSource enemyDeathSoundAudioSource;
    
    
    [SerializeField] private AudioClip[] boneSounds;
    
    [SerializeField] private AudioSource boneSoundAudioSource;
    
    #endregion

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;
           s.source.loop = s.isLoop;
           s.source.playOnAwake = s.playOnAwake;
           s.source.volume = s.volume;

           switch (s.audioType)
           {
               case Sound.AudioTypes.soundEffect:
                   s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                   break;
               
               case Sound.AudioTypes.music:
                   s.source.outputAudioMixerGroup = musicMixerGroup;
                   break;
           }
           
           if (s.playOnAwake)
           {
               s.source.Play();
           }
        }
    }

    public void PlayClipByName(string _clipName)
    {
        Sound soundToPlay = Array.Find(sounds, dummySound => dummySound.clipName == _clipName);

        if (soundToPlay != null)
        {
            soundToPlay.source.Play();
        }
    }
    
    public void StopClipByName(string _clipName)
    {
        Sound soundToStop = Array.Find(sounds, dummySound => dummySound.clipName == _clipName);
        
        if (soundToStop != null)
        {
            soundToStop.source.Stop();
        }
    }

    public void PlayerSoundRun()
    {
        footSteps.PlayOneShot(stepSounds[Random.Range(0, 4)]);
        
        
    }
    
    public void PlayerSoundJump()
    {
        jumpSoundAudioSource.PlayOneShot(jumpSounds[Random.Range(0, 1)]);
        
        
    }
    
    public void PlayerSoundDeath()
    {
        deathSoundAudioSource.PlayOneShot(deathSounds[Random.Range(0, 1)]);

    }
    
    public void PlayerSoundSword()
    {
        swordSoundAudioSource.PlayOneShot(SwordSounds[Random.Range(0, 2)]);

    }
    
    public void CheckpointSound()
    {
        checkpointSoundAudioSource.PlayOneShot(CheckpointSounds[Random.Range(0, 2)]);

    }

    public void EnemySoundDeath()
    {
        enemyDeathSoundAudioSource.PlayOneShot(enemySounds[Random.Range(0, 2)]);
    }

    public void BoneSound()
    {
        boneSoundAudioSource.PlayOneShot(boneSounds[Random.Range(0, 2)]);
    }
}
