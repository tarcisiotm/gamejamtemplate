using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
