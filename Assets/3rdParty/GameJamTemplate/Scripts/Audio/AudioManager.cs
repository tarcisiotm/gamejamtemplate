using UnityEngine;
using DG.Tweening;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// A manager for fading audio, creating new instances, etc
    /// </summary>
    public class AudioManager : TG.Core.Audio.AudioManagerBase
    {
        [Header("Settings")]
        [SerializeField] float _fadeOutTime = 1f;

        public void FadeOutBGM()
        {
            if (_bgmAudioSource == null || _bgmAudioSource.clip == null || !_bgmAudioSource.isPlaying) return;
            _bgmAudioSource.DOFade(0, _fadeOutTime).SetUpdate(true);
        }
    }
}