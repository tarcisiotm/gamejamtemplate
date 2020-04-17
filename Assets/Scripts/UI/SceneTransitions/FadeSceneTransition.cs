using System.Collections;
using System.Collections.Generic;
using TG.Core;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// A simple scene transition with fade-in and out
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class FadeSceneTransition : SceneTransition
{
    [SerializeField] float fadeDuration = .3f;

    [Header("Optional")]
    [Tooltip("Cosmetic object to activate after fade in completes.")]
    [SerializeField] CanvasGroupFadeComponent activateAfterFadeIn = default;

    private void Awake() {
    }

    private void OnEnable() {
        ScenesManager.OnTransitionFadedIn += OnFadedIn;
        ScenesManager.OnTransitionIsGoingToFadeOut += BeforeFadeOut;
    }

    private void OnDisable() {
        ScenesManager.OnTransitionFadedIn -= OnFadedIn;
        ScenesManager.OnTransitionIsGoingToFadeOut -= BeforeFadeOut;
    }

    void Start() {
    }

    public override void FadeIn() {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Fade(1, fadeDuration);
    }

    public override void FadeOut() {
        Fade(0, fadeDuration);
    }

    void OnFadedIn() {
        if (activateAfterFadeIn != null) {
            activateAfterFadeIn.gameObject.SetActive(true);
            activateAfterFadeIn.FadeIn(BeforeFadeStallDuration / 2f);
        }
    }

    void BeforeFadeOut() {
        if (activateAfterFadeIn != null) {
            activateAfterFadeIn.FadeOut(BeforeFadeStallDuration);
        }
    }

    public void Fade(float targetAlpha, float duration) {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(targetAlpha, duration).SetEase(Ease.Linear).SetUpdate(true).OnComplete(OnCompleteCallback);
    }

    void OnCompleteCallback() {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
