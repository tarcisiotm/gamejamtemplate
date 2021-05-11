using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// Provides functionality for audio playback on UI buttons
    /// </summary>
    public class AudioButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private AudioSource _audioSource = default;
        [SerializeField] private AudioClip _onHover = default;
        [SerializeField] private AudioClip _clickClip = default;
        [SerializeField] private float _onHoverVolume = .6f;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            _audioSource.volume = 1f;
            _audioSource.PlayOneShot(_clickClip);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _audioSource.volume = _onHoverVolume;
            _audioSource.PlayOneShot(_onHover);
        }
    }
}