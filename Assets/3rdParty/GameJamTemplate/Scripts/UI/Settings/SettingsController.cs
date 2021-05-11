using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private bool _updateAudioInRealTime = true;

    [SerializeField] private SliderController _bgmSlider = default;
    [SerializeField] private SliderController _sfxSlider = default;

    [Header("References")]
    [SerializeField] private AudioMixer _mainAudioMixer = default;

    private Settings _settings;

    protected virtual void OnEnable()
    {
        _settings = new Settings();
        _settings.Load();

        Init();

        if (_updateAudioInRealTime)
        {
            _bgmSlider.Slider.onValueChanged.AddListener(SetVolume);
            _sfxSlider.Slider.onValueChanged.AddListener(SetVolume);
        }
    }

    protected virtual void Init()
    {
        _bgmSlider.Init(_settings.MusicVolume);
        _sfxSlider.Init(_settings.SFXVolume);
    }

    private void OnDisable()
    {
        if (_updateAudioInRealTime)
        {
            _bgmSlider.Slider.onValueChanged.RemoveListener(SetVolume);
            _sfxSlider.Slider.onValueChanged.RemoveListener(SetVolume);
        }

        _settings.SetAudio(_bgmSlider.Value, _bgmSlider.Value);
        _settings.Save();

        _settings = null;
    }

    public void SetVolume(float value)
    {
        var musicVal = Mathf.Clamp(_bgmSlider.Value, 0.0001f, 80);
        var sfxVal = Mathf.Clamp(_sfxSlider.Value, 0.0001f, 80);

        _mainAudioMixer.SetFloat("BGMVolume", Mathf.Log10(musicVal) * 20);
        _mainAudioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVal) * 20);
    }

    public void Reset()
    {
        _settings = new Settings();

        Init();

        _settings.Save();
    }

    public void Close()
    {
        // show confirm popup?
        gameObject.SetActive(false);
    }
}