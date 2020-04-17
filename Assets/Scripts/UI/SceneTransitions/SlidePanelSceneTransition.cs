using UnityEngine;
using DG.Tweening;
using TG.Core;

/// <summary>
/// Slide from the side scene transition
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class SlidePanelSceneTransition : SceneTransition {
    [SerializeField] float fadeDuration = .3f;

    [Header("Optional")]
    [Tooltip("Cosmetic object to activate after fade in completes.")]
    [SerializeField] CanvasGroupFadeComponent activateAfterFadeIn = default;

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
            activateAfterFadeIn.FadeIn(BeforeFadeStallDuration/2f);
        }
    }

    void BeforeFadeOut() {
        if (activateAfterFadeIn != null) {
            activateAfterFadeIn.FadeOut(BeforeFadeStallDuration);
        }
    }

    //public void FadeIn(float duration) {
    //    Fade(Vector2.zero, duration);
    //}
    
    //public void FadeOut(float duration) {
    //    Fade(new Vector2(multiplier * size * transform.localScale.x, 0), duration);
    //    multiplier *= -1;
    //}

    public void Fade(Vector2 targetPos, float duration){
        RectTransform rt = GetComponent<RectTransform>();
        rt.DOAnchorPos(targetPos, duration).SetEase(Ease.Linear).SetUpdate(true);
    }

}