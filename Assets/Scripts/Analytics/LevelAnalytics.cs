using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelAnalytics : MonoBehaviour
{
    [SerializeField] private Tutor _tutor;
    [SerializeField] private Player _player;
    [SerializeField] private Boss _boss;
    [SerializeField] private TryAgain[] _restartButtons;

    private Analytics _analytics;
    private int _levelIndex;
    private float _startTime;

    private void Awake()
    {
        _analytics = Singleton<Analytics>.Instance;
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable()
    {
        _tutor.Completed += OnTutorCompleted;
        _player.PlayerDied += OnPlayerDied;
        _boss.BossDied += OnBossDied;

        foreach (var button in _restartButtons)
            button.Reloading += OnReloading;
    }

    private void OnDisable()
    {
        _tutor.Completed -= OnTutorCompleted;
        _player.PlayerDied -= OnPlayerDied;
        _boss.BossDied -= OnBossDied;

        foreach (var button in _restartButtons)
            button.Reloading += OnReloading;
    }

    private void Start()
    {
        _analytics.FireEvent("main_menu");
    }

    private void OnTutorCompleted()
    {
        var parameters = new Dictionary<string, object>() { { "level", _levelIndex }, };
        _analytics.FireEvent("level_start", parameters);

        _startTime = Time.realtimeSinceStartup;
    }

    private void OnPlayerDied(string damageSource)
    {
        var timeSpent = Time.realtimeSinceStartup - _startTime;
        var parameters = new Dictionary<string, object>() {
            { "level", _levelIndex },
            { "reason", damageSource},
            { "time_spent", (int)timeSpent },
        };

        _analytics.FireEvent("fail", parameters);
    }

    private void OnBossDied(string damageSource)
    {
        var timeSpent = Time.realtimeSinceStartup - _startTime;
        var parameters = new Dictionary<string, object>() {
            { "level", _levelIndex },
            { "time_spent", (int)timeSpent },
        };

        _analytics.FireEvent("level_complete", parameters);
    }

    private void OnReloading()
    {
        var parameters = new Dictionary<string, object>() {
            { "level", _levelIndex },
        };

        _analytics.FireEvent("restart", parameters);
    }
}
