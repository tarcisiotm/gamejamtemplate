using TMPro;
using UnityEngine;

public class UIStringAnimator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [Space]
    [SerializeField] private string[] _textToAnimate;
    [SerializeField] private float _delayBetweenTexts = .3f;
    [SerializeField] private bool _yoyoAnimation = true;

    private float _timer = 0f;
    private int _currentIndex = 0;
    private int _direction = 1;

    void Start()
    {
        _currentIndex = 0;
        _text.text = _textToAnimate[_currentIndex];
    }

    void Update()
    {
        if (_textToAnimate == null || _textToAnimate.Length == 0) { return; }

        _timer += Time.deltaTime;

        if (_timer > _delayBetweenTexts) 
        {
            _timer = 0f;
            Advance();
            _text.text = _textToAnimate[_currentIndex];
        }
    }

    private void Advance()
    {
        if (!_yoyoAnimation)
        {
            _currentIndex = (_currentIndex + 1) % _textToAnimate.Length;
            return;
        }

        _currentIndex += _direction;

        if (_currentIndex >= _textToAnimate.Length)
        {
            _currentIndex = _textToAnimate.Length - 2;
            _direction = -1;
        }
        else if (_currentIndex < 0)
        {
            _currentIndex = 1;
            _direction = 1;
        }
    }
}