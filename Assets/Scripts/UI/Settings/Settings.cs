using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    protected const string PREFS_MUSIC_VOLUME = "MusicVolume";
    protected const string PREFS_SFX_VOLUME = "SFXVolume";

    protected const float DEFAULT_VOLUME = .7f;

    protected float _bgmVolume = DEFAULT_VOLUME;
    protected float _sfxVolume = DEFAULT_VOLUME;

    public float MusicVolume => _bgmVolume;
    public float SFXVolume => _sfxVolume;

    public virtual void Load()
    {
        _bgmVolume = PlayerPrefs.GetFloat(PREFS_MUSIC_VOLUME, DEFAULT_VOLUME);
        _sfxVolume = PlayerPrefs.GetFloat(PREFS_SFX_VOLUME, DEFAULT_VOLUME);
    }

    public virtual void Save()
    {
        PlayerPrefs.SetFloat(PREFS_MUSIC_VOLUME, _bgmVolume);
        PlayerPrefs.SetFloat(PREFS_SFX_VOLUME, _sfxVolume);
    }

    public void SetAudio(float musicVolume, float sfxVolume)
    {
        _bgmVolume = musicVolume;
        _sfxVolume = sfxVolume;
    }
}