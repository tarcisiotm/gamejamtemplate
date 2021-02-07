using System.Collections;
using UnityEngine;

/// <summary>
/// A simple BGM handler script to change the music
/// </summary>
public class ChangeBGMOnEnable : MonoBehaviour
{
    [SerializeField] AudioClip bgmClip = default;
    [SerializeField] float volume = 1;
    [SerializeField] bool autoDestroy = true;

    private void OnEnable()
    {
        if (AudioManager.I != null) PlayBGM();
        else StartCoroutine(WaitForAudioManager());
    }

    private IEnumerator WaitForAudioManager()
    {
        while (AudioManager.I == null) yield return null;

        PlayBGM();

        if (autoDestroy)
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM()
    {
        AudioManager.I.PlayBGM(bgmClip, volume);
    }
}