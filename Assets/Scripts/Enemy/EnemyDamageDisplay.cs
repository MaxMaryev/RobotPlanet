using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyDamageDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro _damageText;
    [SerializeField] private float _defaultScale;
    [SerializeField] private float _targetScale;
    [SerializeField] private float _scalingSpeed;

    private IDamageable _enemy;
    private Sequence _textAnimationSequence;
    private int _damageDisplays;
    private Coroutine _hideTextCoroutine;
    private WaitForSeconds _hidingDelay;
    private Color _defaultColor;
    private float _defaultFontSize;
    private float _stackedDamage;

    private void Awake()
    {
        _enemy = GetComponent<IDamageable>();
        _defaultColor = Color.white;
        _defaultFontSize = _damageText.fontSize;
        _hidingDelay = new WaitForSeconds(0.5f);
    }

    private void OnEnable()
    {
        _enemy.Damaged += OnShow;
    }

    private void OnDisable()
    {
        _enemy.Damaged -= OnShow;

        SetDefaultSettings();
    }

    public void Init()
    {
        _enemy = GetComponent<IDamageable>();
    }

    private void OnShow(float damage)
    {
        if (_textAnimationSequence.IsActive())
            return;

        _stackedDamage += damage;
        float totalDamageRecevied = Mathf.RoundToInt(_stackedDamage);
        Vector3 cameraDirection = transform.position - Camera.main.transform.position;

        _damageText.gameObject.SetActive(true);
        _damageText.transform.rotation = Quaternion.LookRotation(cameraDirection);

        ShowRepeatedDamageEffect();
        _damageText.text = $"{totalDamageRecevied}";

        _textAnimationSequence = DOTween.Sequence()
                .Append(_damageText.transform.DOScale(_targetScale, _scalingSpeed))
                .Append(_damageText.transform.DOScale(_defaultScale, _scalingSpeed))
                .OnComplete(() => TryHideText());

        _damageDisplays++;

    }
    private void ShowRepeatedDamageEffect()
    {
        int stepsCount = 10;
        float step = 0.1f;
        float colorAlpha = 0.75f;

        if (_damageDisplays <= stepsCount)
        {
            _damageText.color = new Color(_defaultColor.r, _defaultColor.g - step * _damageDisplays, _defaultColor.b, colorAlpha);
            _damageText.fontSize += step;
        }
    }

    private void TryHideText()
    {
        if (_hideTextCoroutine != null)
            StopCoroutine(_hideTextCoroutine);

        if (isActiveAndEnabled)
            _hideTextCoroutine = StartCoroutine(HideTextCoroutine());

        IEnumerator HideTextCoroutine()
        {
            yield return _hidingDelay;
            _damageText.gameObject.SetActive(false);

            SetDefaultSettings();
        }

    }

    private void SetDefaultSettings()
    {
        _damageDisplays = 0;
        _damageText.fontSize = _defaultFontSize;
        _damageText.color = _defaultColor;
        _damageText.gameObject.SetActive(false);
        _stackedDamage = 0;
    }
}
