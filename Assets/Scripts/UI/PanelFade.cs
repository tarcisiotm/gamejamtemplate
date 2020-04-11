using UnityEngine;
using DG.Tweening;
using TG.Core;
using System;

[RequireComponent(typeof(RectTransform))]
public class PanelFade : SceneTransition {
    [SerializeField] float fadeDuration = .3f;

    [Header("Optional")]
    [Tooltip("Cosmetic object to activate after fade in completes.")]
    [SerializeField] CanvasGroupFadeComponent activateAfterFadeIn;

    [SerializeField] float size = 0;
    int multiplier = -1;

    private void Awake()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * transform.localScale.x, 0);
        size = size > 0 ? size : Screen.width;
    }

    private void OnEnable() {
        ScenesManager.OnTransitionFadedIn += OnFadedIn;
        ScenesManager.OnTransitionIsGoingToFadeOut += BeforeFadeOut;
    }

    private void OnDisable() {
        ScenesManager.OnTransitionFadedIn -= OnFadedIn;
        ScenesManager.OnTransitionIsGoingToFadeOut -= BeforeFadeOut;
    }

    void Start(){
        //DontDestroyOnLoad(GetComponentInParent<Canvas>().gameObject);
        //FadeIn(2f);
    }

    public override void FadeIn() {
        Fade(Vector2.zero, fadeDuration);
    }

    public override void FadeOut() {
        Fade(new Vector2(multiplier * size * transform.localScale.x, 0), fadeDuration);
        multiplier *= -1;
    }

    void OnFadedIn() {
        if(activateAfterFadeIn != null) {
            activateAfterFadeIn.gameObject.SetActive(true);
            activateAfterFadeIn.FadeIn(BeforeFadeStallDuration);
        }
    }

    void BeforeFadeOut() {
        activateAfterFadeIn.FadeOut(BeforeFadeStallDuration);
    }

    public void FadeIn(float duration) {
        //Debug.Log("Fade in");
        Fade(Vector2.zero, duration);
    }
    
    public void FadeOut(float duration) {
        //var vect2 = Screen.width;
        //Debug.Log("Fade out");
        Fade(new Vector2(multiplier * size * transform.localScale.x, 0), duration);
        multiplier *= -1;
    }

    public void Fade(Vector2 targetPos, float duration){
        RectTransform rt = GetComponent<RectTransform>();
        rt.DOAnchorPos(targetPos, duration).SetEase(Ease.Linear);
    }


}
