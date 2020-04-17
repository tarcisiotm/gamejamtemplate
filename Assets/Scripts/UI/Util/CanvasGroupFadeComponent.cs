using UnityEngine;
using DG.Tweening;

/// <summary>
/// A handler for fading a Canvas Group
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFadeComponent : MonoBehaviour
{
    public void FadeIn(float duration) {
        GetComponentInChildren<CanvasGroup>().DOFade(1, duration);
    }

    public void FadeOut(float duration) {
        GetComponentInChildren<CanvasGroup>().DOFade(0, duration).OnComplete(DisableGameObject);
    }

    void DisableGameObject() {
        gameObject.SetActive(false);
    }
}