using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerHealtDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro _healtBoostText;
    [SerializeField] private float _defaultScale;
    [SerializeField] private float _targetScale;
    [SerializeField] private float _scalingSpeed;
    [SerializeField] private Player _player;

    private Sequence _textAnimationSequence;
    private float _defaultFontSize;
    private float _previousHealthValue;

    private void Awake()
    {
        _previousHealthValue = _player.MaxHealth;
        _healtBoostText.color = Color.green;
        _defaultFontSize = _healtBoostText.fontSize;
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnShow;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnShow;
        SetDefaultSettings();
    }

    private void OnShow(float currentHealt)
    {
        if (_textAnimationSequence.IsActive())
            return;

        float healtChange = currentHealt - _previousHealthValue;
        _previousHealthValue = currentHealt;
        if (healtChange > 0)
        {
            float totalHealtAdd = Mathf.RoundToInt(healtChange);
            Vector3 cameraDirection = transform.position - Camera.main.transform.position;
            _healtBoostText.gameObject.SetActive(true);
            _healtBoostText.transform.rotation = Quaternion.LookRotation(cameraDirection);
            _healtBoostText.text = $"+{totalHealtAdd}";
            _textAnimationSequence = DOTween.Sequence()
                    .Append(_healtBoostText.transform.DOScale(_targetScale, _scalingSpeed))
                    .Append(_healtBoostText.transform.DOScale(_defaultScale, _scalingSpeed))
                    .OnComplete(() => TryHideText());
        }
    }

    private void TryHideText()
    {
        _healtBoostText.gameObject.SetActive(false);
        SetDefaultSettings();
    }

    private void SetDefaultSettings()
    {
        _healtBoostText.fontSize = _defaultFontSize;
        _healtBoostText.gameObject.SetActive(false);
    }
}