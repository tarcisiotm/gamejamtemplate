using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Provides functionality for audio playback on UI buttons
/// </summary>
public class AudioButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioSource audioSource = default;
    [SerializeField] AudioClip onHover = default;
    [SerializeField] float onHoverVolume = .6f;
    [SerializeField] AudioClip clickClip = default;

    private void Awake()
    {

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(clickClip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.volume = onHoverVolume;
        audioSource.PlayOneShot(onHover);
    }
}
