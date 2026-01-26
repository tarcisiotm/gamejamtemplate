using TG.Core;
using UnityEngine;
using DG.Tweening;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// A simple scene transition with fade-in and out
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeSceneTransition : SceneTransition
    {
        [SerializeField] float _fadeDuration = .3f;

        [Header("Optional")]
        [Tooltip("UI object to activate after fade in completes.")]
        [SerializeField] CanvasGroupFadeComponent _activateAfterFadeIn = default;

        private void Awake()
        {
        }

        private void Start()
        {
        }

        public override void FadeIn()
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            Fade(1, _fadeDuration);
        }

        public override void FadeOut()
        {
            Fade(0, _fadeDuration);
        }

        protected override void OnFadedIn()
        {
            if (_activateAfterFadeIn != null)
            {
                _activateAfterFadeIn.gameObject.SetActive(true);
                _activateAfterFadeIn.FadeIn(BeforeFadeStallDuration / 2f);
            }
        }

        protected override void BeforeFadeOut()
        {
            if (_activateAfterFadeIn != null)
            {
                _activateAfterFadeIn.FadeOut(BeforeFadeStallDuration);
            }
        }

        protected void Fade(float targetAlpha, float duration)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            fadeTween = canvasGroup.DOFade(targetAlpha, duration)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(OnCompleteCallback);
                //.OnUpdate(OnFadeUpdate);
        }

        //protected void OnFadeUpdate() 
        //{
            //Debug.LogError($"{fadeTween.ElapsedPercentage()}%");
        //}

        private void OnCompleteCallback()
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}