using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _fadeInTime = 1;
    [SerializeField] private float _fadeOutTime = 1;
    [Tooltip("Delay after fade in, before scrolling.")]
    [SerializeField] private float _preScrollingDelay = 3f;
    [Tooltip("Delay after scrolling, before fading out.")]
    [SerializeField] private float _postScrollingDelay = 5f;
    [SerializeField] private float _scrollingSpeed = 10;

    [Header("References")]
    [SerializeField] private CreditsData[] _creditsData;
    [SerializeField] private GameObject _pressAgainToQuitText;

    [Header("Debug")]
    [SerializeField] private float _currentTime = 0;
    [SerializeField] private CreditsState _creditsState = CreditsState.FadingIn;

    private float _targetTime = 0;

    private int _keysPressed = 0;

    private float _targetYOffset;

    private enum CreditsState
    {
        FadingIn,
        PostFadeIn,
        Scrolling,
        PreFadeOut,
        FadingOut
    }

    private CanvasGroup _canvasGroup;
    private RectTransform _creditsPanelTransform;

    private GameObject _titleTemplate;
    private GameObject _nameTemplate;
    private GameObject _linkTemplate;
    private GameObject _spaceTemplate;

    private Transform _thanksText;

    private Vector3 _initialPos;
    private Vector3 _targetPos;

    private bool _hasInit;

    private void Reset()
    {
        _currentTime = 0;
        _targetTime = _fadeInTime;

        _keysPressed = 0;
        _creditsPanelTransform.anchoredPosition = Vector2.zero;
        _creditsState = CreditsState.FadingIn;
    }

    private void OnEnable()
    {
        _pressAgainToQuitText.SetActive(false);

        if (!_hasInit)
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            _targetTime = _fadeInTime;

            if (_fadeInTime > 0) _canvasGroup.alpha = 0;

            _creditsPanelTransform = transform.Find("Credits Panel").GetComponent<RectTransform>();
            _titleTemplate = _creditsPanelTransform.Find("Templates/Title Template").gameObject;
            _nameTemplate = _creditsPanelTransform.Find("Templates/Name Template").gameObject;
            _linkTemplate = _creditsPanelTransform.Find("Templates/Link Template").gameObject;
            _spaceTemplate = _creditsPanelTransform.Find("Templates/Space Template").gameObject;
            _thanksText = _creditsPanelTransform.Find("Thanks Text");

            BuildCredits();

            _thanksText.SetAsLastSibling();
        }
        else Reset();
    }

    private IEnumerator Start()
    {
        yield return null;

        Reset();

        _hasInit = true;
    }

    private void Update()
    {
        if (!_hasInit) return;

        if (Input.anyKeyDown && _creditsState != CreditsState.FadingOut)
        {
            if (_keysPressed < 1)
            {
                _keysPressed++;
                _pressAgainToQuitText.SetActive(true);
            }
            else SetupFadeOut();
        }

        FadeIn();
        Delay(CreditsState.PostFadeIn, SetupScrolling);
        HandleScrolling();
        Delay(CreditsState.PreFadeOut, SetupFadeOut);
        FadeOut();
    }
 
    #region Creation
    private void BuildCredits()
    {
        foreach (var creditData in _creditsData) BuildCredit(creditData);
    }

    private void BuildCredit(CreditsData creditsData)
    {
        if (creditsData == null) return;
        if (!string.IsNullOrEmpty(creditsData.Title)) InstantiateEntry(_titleTemplate, "Title: ", creditsData.Title);

        foreach (var entry in creditsData.CreditsEntries)
        {
            InstantiateEntry(_nameTemplate, "Name: ", entry.Name);

            if (entry.Links != null && entry.Links.Length != 0)
            {
                foreach (var link in entry.Links) InstantiateEntry(_linkTemplate, "Link: ", link);
            }

            var space = Instantiate(_spaceTemplate).gameObject;
            space.transform.SetParent(_creditsPanelTransform);
            space.gameObject.SetActive(true);
            space.gameObject.name = "[Space]";
        }
    }

    private void InstantiateEntry(GameObject template, string category, string text, Transform parent = null)
    {
        if (template == null || string.IsNullOrEmpty(text)) return;

        var element = Instantiate(template).GetComponent<TMPro.TextMeshProUGUI>();
        element.text = text;

        parent = parent == null ? _creditsPanelTransform : parent;
        element.transform.SetParent(parent, false);

        element.gameObject.SetActive(true);

        element.gameObject.name = category + text;
    }
    #endregion Creation

    private void Fade(float initialAlpha, float targetAlpha)
    {
        _currentTime += Time.deltaTime;
        _canvasGroup.alpha = Mathf.Lerp(initialAlpha, targetAlpha, _currentTime / _targetTime);
    }

    private void FadeIn()
    {
        if (_creditsState != CreditsState.FadingIn) return;

        Fade(0, 1);
        if (_currentTime >= _targetTime)
        {
            _currentTime = 0;
            _creditsState++;
        }
    }

    private void SetupScrolling()
    {
        _initialPos = _creditsPanelTransform.anchoredPosition;

        var ySizeDelta = _creditsPanelTransform.GetComponent<RectTransform>().sizeDelta.y;
        _targetYOffset = Mathf.Abs(ySizeDelta - 540f); // canvas height / 2f

        _targetPos = _initialPos + new Vector3(0, _targetYOffset, 0);
        _targetTime = _preScrollingDelay + ySizeDelta / _scrollingSpeed;
        _currentTime = 0;

        _creditsState = CreditsState.Scrolling;
    }

    private void HandleScrolling()
    {
        if (_creditsState != CreditsState.Scrolling) return;

        _currentTime += _scrollingSpeed * Time.deltaTime;

        _creditsPanelTransform.anchoredPosition = Vector2.Lerp(_initialPos, _targetPos, _currentTime / _targetTime);

        if (_creditsPanelTransform.anchoredPosition.y == _targetPos.y)
        {
            _currentTime = 0;
            _targetTime = _postScrollingDelay;
            _creditsState++;
        }
    }

    private void Delay(CreditsState state, System.Action OnDelayIsDone = null)
    {
        if (_creditsState != state) return;
        _currentTime += Time.deltaTime;

        if (_currentTime >= _targetTime) OnDelayIsDone?.Invoke();
    }

    private void SetupFadeOut()
    {
        _creditsState = CreditsState.FadingOut;
        _currentTime = 0;
        _targetTime = _fadeOutTime;
    }

    private void FadeOut()
    {
        if (_creditsState != CreditsState.FadingOut) return;

        Fade(1, 0);
        if (_currentTime >= _targetTime) gameObject.SetActive(false); // TODO set button focus back to credits button
    }
}