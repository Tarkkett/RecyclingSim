using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicDB, sfxDB;
    public AudioSource musicSource, sfxSource;

    public static AudioManager instance;

    private void Awake()
    {
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

    private void Start()
    {
        PlayMusic("Theme");
        musicSource.mute = false;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicDB, x => x.name == name);

        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.volume = 0.5f;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxDB, x => x.name == name);

        if (s != null)
        {
            sfxSource.PlayOneShot(s.clip);
            sfxSource.volume = 0.4f;
        }
    }
}
