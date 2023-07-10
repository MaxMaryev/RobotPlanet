using System;
using UnityEngine;
using UnityEngine.UI;
using BlobArena.Model;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _filledImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _closeImage;
    [SerializeField] private AbilityIcons _icons;

    private AbilityPresenter _ability;
    private ITimer _timer;
    private float _fullTime;

    public bool Initialized => _ability != null;

    private void Awake()
    {
        OnTimerCompleted();
    }

    private void OnDisable()
    {
        if (_timer != null)
        {
            _timer.Started -= OnTimerStart;
            _timer.Updated -= OnTimerUpdate;
            _timer.Completed -= OnTimerCompleted;
        }
    }

    public void Render(Sprite background)
    {
        _backgroundImage.sprite = background;
        _backgroundImage.color = Color.white;
    }

    public void Init(AbilityPresenter ability)
    {
        if (Initialized)
            throw new InvalidOperationException("Already initialized");

        _ability = ability;
        _closeImage.gameObject.SetActive(false);
        _iconImage.sprite = _icons.GetIconBy(ability.AbilityInfo.Identifier);

        if (_ability.HasTimer)
        {
            _timer = (_ability as IUpdatable).Timer;

            _timer.Started += OnTimerStart;
            _timer.Updated += OnTimerUpdate;
            _timer.Completed += OnTimerCompleted;
        }
    }

    private void OnTimerStart(float fullTime)
    {
        _fullTime = fullTime;
    }

    private void OnTimerUpdate(float ellapsedTime)
    {
        _filledImage.fillAmount = Mathf.Lerp(1f, 0f, ellapsedTime / _fullTime);
    }

    private void OnTimerCompleted()
    {
        _filledImage.fillAmount = 0f;
    }
}
