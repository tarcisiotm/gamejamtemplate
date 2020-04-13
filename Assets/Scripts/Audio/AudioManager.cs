using UnityEngine;
using DG.Tweening;

/// <summary>
/// A manager for fading audio, creating new instances, etc
/// </summary>
public class AudioManager : TG.Core.Audio.AudioManagerBase
{
    [Header("Settings")]
    [SerializeField] float fadeOutTime = 1f;

    protected override void OnSceneIsGoingToLoad(int activeSceneBuildIndex, int newSceneBuildIndex) {
        base.OnSceneIsGoingToLoad(activeSceneBuildIndex, newSceneBuildIndex);

        if(bgmAudioSource != null) {
            bgmAudioSource.DOFade(0, fadeOutTime).SetUpdate(true);
        }
    }
}
