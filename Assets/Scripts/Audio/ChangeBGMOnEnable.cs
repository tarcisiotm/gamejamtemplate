using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGMOnEnable : MonoBehaviour
{
    [SerializeField] AudioClip bgmClip = default;
    [SerializeField] float volume = 1;

    public void PlayBGM() {
        AudioManager.I.PlayBGM(bgmClip, volume);
    }

    void Awake()
    {
        PlayBGM();
    }

    void Update()
    {
        
    }
}
