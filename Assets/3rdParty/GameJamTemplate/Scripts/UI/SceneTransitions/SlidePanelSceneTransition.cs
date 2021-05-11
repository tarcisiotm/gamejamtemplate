using UnityEngine;
using DG.Tweening;
using TG.Core;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// Slide from the side scene transition
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class SlidePanelSceneTransition : SceneTransition
    {
        [SerializeField] float _fadeDuration = .3f;

        [Header("Optional")]
        [Tooltip("Cosmetic object to activate after fade in completes.")]
        [SerializeField] CanvasGroupFadeComponent _activateAfterFadeIn = default;

        [SerializeField] private float _size = 0;
        private int _multiplier = -1;

        private void Awake()
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * transform.localScale.x, 0);
            _size = _size > 0 ? _size : Screen.width;
        }

        private void OnEnable()
        {
            ScenesManager.OnTransitionFadedIn += OnFadedIn;
            ScenesManager.OnTransitionIsGoingToFadeOut += BeforeFadeOut;
        }

        private void OnDisable()
        {
            ScenesManager.OnTransitionFadedIn -= OnFadedIn;
            ScenesManager.OnTransitionIsGoingToFadeOut -= BeforeFadeOut;
        }

        private void Start()
        {
            //FadeIn(2f);
        }

        public override void FadeIn()
        {
            Fade(Vector2.zero, _fadeDuration);
        }

        public override void FadeOut()
        {
            Fade(new Vector2(_multiplier * _size * transform.localScale.x, 0), _fadeDuration);
            _multiplier *= -1;
        }

        private void OnFadedIn()
        {
            if (_activateAfterFadeIn != null)
            {
                _activateAfterFadeIn.gameObject.SetActive(true);
                _activateAfterFadeIn.FadeIn(BeforeFadeStallDuration / 2f);
            }
        }

        private void BeforeFadeOut()
        {
            if (_activateAfterFadeIn != null)
            {
                _activateAfterFadeIn.FadeOut(BeforeFadeStallDuration);
            }
        }

        //public void FadeIn(float duration) {
        //    Fade(Vector2.zero, duration);
        //}

        //public void FadeOut(float duration) {
        //    Fade(new Vector2(multiplier * size * transform.localScale.x, 0), duration);
        //    multiplier *= -1;
        //}

        public void Fade(Vector2 targetPos, float duration)
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.DOAnchorPos(targetPos, duration).SetEase(Ease.Linear).SetUpdate(true);
        }
    }
}