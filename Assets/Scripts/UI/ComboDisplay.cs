using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class ComboDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _comboTimeWindow;
    [SerializeField] private float _defaultScale;
    [SerializeField] private float _targetScale;
    [SerializeField] private float _scalingSpeed;
    [SerializeField] private float _maxFontSize;

    private int _combo;
    private float _defaultFontSize;
    private Color _defaultColor;
    private float _lastKillTime;
    private float _previousKillTime;
    private Coroutine _show;
    WaitForSeconds _waitForSeconds;
    private Sequence _textAnimationSequence;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_comboTimeWindow);
        _defaultFontSize = _text.fontSize;
        _defaultColor = _text.color;
    }

    private void Start()
    {
        SetDefaultSettings();
    }

    private void OnEnable()
    {
        _enemySpawner.EnemyDied += OnUpdateKills;
    }

    private void OnDisable()
    {
        _enemySpawner.EnemyDied -= OnUpdateKills;
    }

    private void OnUpdateKills()
    {
        _previousKillTime = _lastKillTime;
        _lastKillTime = Time.time;

        if (_lastKillTime - _previousKillTime <= _comboTimeWindow || _combo == 0)
        {
            if (_show != null)
                StopCoroutine(_show);

            ++_combo;
            _show = StartCoroutine(Show());
        }
    }

    private IEnumerator Show()
    {
        SetFontSize();
        SetColor();

        _text.text = $"x{_combo}";

        Pulsate(_targetScale, _defaultScale, _scalingSpeed);

        yield return _waitForSeconds;
        Pulsate(_targetScale, 0, _scalingSpeed);

        yield return _waitForSeconds;
        SetDefaultSettings();
    }
    
    private void Pulsate(float targetScale, float endScale, float speed)
    {
        _textAnimationSequence = DOTween.Sequence()
        .Append(_text.rectTransform.DOScale(targetScale, speed))
        .Append(_text.rectTransform.DOScale(endScale, speed));
    }

    private void SetFontSize()
    {
        float divider = 20;

        if (_text.fontSize < _maxFontSize)
            _text.fontSize += _combo / divider;
    }

    private void SetColor()
    {
        float divider = 100f;
        _text.color = Color.Lerp(Color.white, Color.red, _combo / divider);
    }

    private void SetDefaultSettings()
    {
        _combo = 0;
        _text.text = $"";
        _text.fontSize = _defaultFontSize;
        _text.color = _defaultColor;
    }
}
