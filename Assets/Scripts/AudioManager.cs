using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioConfigSO settings;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource walkSource;

    [Header("Audio Clips")]
    [SerializeField] private List<AudioClip> musicTracks;
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private AudioClip walkClip; 

    private int currentMusicIndex = 0;

    private void Start()
    {
        ApplySettings();
        PlayMusic(currentMusicIndex);

        if (walkSource != null && walkClip != null)
        {
            walkSource.clip = walkClip;
            walkSource.loop = true;
            walkSource.outputAudioMixerGroup = sfxSource.outputAudioMixerGroup; 
        }
    }

    public void ApplySettings()
    {
        mixer.SetFloat("MasterVolume", settings.masterVolume);
        mixer.SetFloat("MusicVolume", settings.musicVolume);
        mixer.SetFloat("SFXVolume", settings.sfxVolume);
    }

    public void PlayMusic(int index)
    {
        if (musicTracks == null || musicTracks.Count == 0)
        {
            return;
        }

        if (index < 0 || index >= musicTracks.Count)
        {
            return;
        }

        currentMusicIndex = index;
        musicSource.clip = musicTracks[index];
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (!musicSource.isPlaying && musicSource.clip != null)
            musicSource.Play();
    }

    public void NextMusic()
    {
        int nextIndex = (currentMusicIndex + 1) % musicTracks.Count;
        PlayMusic(nextIndex);
    }

    public void PreviousMusic()
    {
        int prevIndex = (currentMusicIndex - 1 + musicTracks.Count) % musicTracks.Count;
        PlayMusic(prevIndex);
    }

    public void PlaySFX(int index)
    {
        if (sfxClips == null || sfxClips.Count == 0)
        {
            return;
        }
        if (index < 0 || index >= sfxClips.Count)
        {
            return;
        }

        sfxSource.clip = sfxClips[index];
        sfxSource.loop = false;
        sfxSource.Play();
    }

    public void PlaySFXOneShot(int index)
    {
        if (sfxClips == null || sfxClips.Count == 0)
        {
            return;
        }
        if (index < 0 || index >= sfxClips.Count)
        {
            return;
        }

        sfxSource.PlayOneShot(sfxClips[index]);
    }

    public void PlayWalkLoop()
    {
        if (walkSource != null && !walkSource.isPlaying)
            walkSource.Play();
    }

    public void StopWalkLoop()
    {
        if (walkSource != null && walkSource.isPlaying)
            walkSource.Stop();
    }
}