using System;
using UnityEngine;
using UnityEngine.Events;

[Obsolete("Not used any more")]
[RequireComponent(typeof(Collider))]
public class ExperienceCollector : MonoBehaviour
{
    [SerializeField] private ParticleSystem _levelUpEffect;
    [SerializeField] private Magnit _experienceMagnit;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private int _startExperienceCount;
    [SerializeField] private int _endExperienceCount;
    [SerializeField] private int _maxLevel;

    private int _experience;

    public event UnityAction LevelReceived;
    public event UnityAction<int> Collected;

    public int MaxExperience => _startExperienceCount + (int)(_curve.Evaluate((float)Level / _maxLevel) * _endExperienceCount);
    public int Level { get; private set; } = 1;

    private void OnValidate()
    {
        AnimationCurveUtils.Normalize(ref _curve);
    }

    private void Awake()
    {
        Collected?.Invoke(0);
    }

    private void OnEnable()
    {
        _experienceMagnit.Attracted += OnExperienceAttracted;
    }

    private void OnDisable()
    {
        _experienceMagnit.Attracted -= OnExperienceAttracted;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Experience experience))
        {
            _experienceMagnit.Attract(experience);
        }
    }

    private void OnExperienceAttracted(int experience)
    {
        _experience += experience;
        LevelUp();
        Collected?.Invoke(_experience);
    }

    private void LevelUp()
    {
        if (_experience >= MaxExperience)
        {
            Level += 1;
            _experience -= MaxExperience;
            _levelUpEffect.Play();
            LevelReceived?.Invoke();
        }
    }
}
