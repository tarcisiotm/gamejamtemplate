using System.Collections;
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
            if (GameManager.I != null && GameManager.I.Initialized) PlayBGM();
            else GameManager.OnInitialized += PlayBGM;
        }

        private void OnDisable()
        {
            GameManager.OnInitialized -= PlayBGM;
        }

        public void PlayBGM()
        {
            if (_bgmClip == null)
            {
                Debug.LogError($"BGM is null on {name}. This is probably unintended.");
                return;
            }

            var audioManager = GameManager.I.GetModule<AudioManager>();
            audioManager.PlayBGM(_bgmClip, gameObject.scene.buildIndex, _volume, _delay);

            if (_autoDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}