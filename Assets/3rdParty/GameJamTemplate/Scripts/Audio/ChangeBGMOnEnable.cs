using System.Collections;
using TG.Core;
using UnityEngine;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// A simple BGM handler script to change the music
    /// </summary>
    public class ChangeBGMOnEnable : MonoBehaviour
    {
        [SerializeField] AudioClip _bgmClip = default;
        [SerializeField] [Range(0,1f)] float _volume = 1;
        [SerializeField] float _delay = 0f;
        [SerializeField] bool _autoDestroy = true;

        private void OnEnable()
        {
            if (GameManager.I != null && GameManager.I.HasFullyInitialized) PlayBGM();
            else StartCoroutine(WaitForAudioManager());
        }

        private IEnumerator WaitForAudioManager()
        {
            if (_bgmClip == null)
            {
                Debug.LogError($"BGM is null on {name}. This is probably unintended.");
                yield break;
            }

            while (GameManager.I == null || !GameManager.I.HasFullyInitialized) yield return null;

            PlayBGM();

            if (_autoDestroy)
            {
                Destroy(gameObject);
            }
        }

        public void PlayBGM()
        {
            var audioManager = GameManager.I.GetModule<AudioManager>();
            audioManager.PlayBGM(_bgmClip, _volume);
        }
    }
}