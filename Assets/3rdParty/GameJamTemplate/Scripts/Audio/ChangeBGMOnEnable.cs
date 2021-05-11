using System.Collections;
using UnityEngine;

/// <summary>
/// A simple BGM handler script to change the music
/// </summary>
public class ChangeBGMOnEnable : MonoBehaviour
{
    [SerializeField] AudioClip _bgmClip = default;
    [SerializeField] float _volume = 1;
    [SerializeField] float _delay = 0f;
    [SerializeField] bool autoDestroy = true;

    private void OnEnable()
    {
        if (AudioManager.I != null) PlayBGM();
        else StartCoroutine(WaitForAudioManager());
    }

    private IEnumerator WaitForAudioManager()
    {
        if (_bgmClip == null)
        {
            Debug.LogError($"BGM is null on {name}. This is probably unintended.");
            yield break;
        }

        yield return new WaitForSeconds(_delay);

        while (AudioManager.I == null) yield return null;

        PlayBGM();

        if (autoDestroy)
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM()
    {
        AudioManager.I.PlayBGM(_bgmClip, _volume);
    }
}