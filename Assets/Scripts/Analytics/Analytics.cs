using System;
using System.Collections.Generic;
using UnityEngine;

public class Analytics : Singleton<Analytics>
{
    private const string RegDayKey = "RegDay";
    private const string SessionCountKey = "SessionCount";

    private AnalyticsPlayTimeLogger _timeLogger;

    public TimeSpan AllPlayTime => _timeLogger.AllPlayTime;

    private string _regDay
    {
        get { return PlayerPrefs.GetString(RegDayKey, DateTime.Today.ToString()); }
        set { PlayerPrefs.SetString(RegDayKey, value); }
    }

    private int _sessionCount
    {
        get { return PlayerPrefs.GetInt(SessionCountKey, 0); }
        set { PlayerPrefs.SetInt(SessionCountKey, value); }
    }

    protected override void OnAwake()
    {
        _timeLogger = Singleton<AnalyticsPlayTimeLogger>.Instance;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(RegDayKey) == false)
        {
            DateTime regDay = DateTime.Today;
            _regDay = regDay.ToString();

            YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
            userProfile.Apply(YandexAppMetricaAttribute.CustomString("dd/MM/yy").WithValue(_regDay));
            AppMetrica.Instance.SetUserProfileID(new DuckyID().Value());
            AppMetrica.Instance.ReportUserProfile(userProfile);
        }

        _sessionCount += 1;
        SetBasicProperty(_sessionCount);
        
        FireEvent("game_start", new Dictionary<string, object>() { { "count", _sessionCount } });
    }

    public void LogTime(int elapsedMinutes)
    {
        var properties = new Dictionary<string, object>()
        {
            {"elapsed_minutes", elapsedMinutes}
        };

        FireEvent("play_time", properties);
    }

    public void FireEvent(string eventName, Dictionary<string, object> eventProps = null)
    {
        eventProps ??= new Dictionary<string, object>();

        eventProps.Add("total_playtime_min", AllPlayTime.Minutes);
        eventProps.Add("total_playtime_sec", AllPlayTime.Minutes * 60 + AllPlayTime.Seconds);

#if UNITY_EDITOR
        Debug.Log($"FireEvent: {eventName},\n{string.Join(Environment.NewLine ,eventProps)}");
#endif

        AppMetrica.Instance.ReportEvent(eventName, eventProps);
    }

    private void SetBasicProperty(int sessionCount)
    {
        int daysInGame = DateTime.Today.Subtract(DateTime.Parse(_regDay)).Days;

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("days_in_game").WithDelta(daysInGame));
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("session_count").WithDelta(sessionCount));

        AppMetrica.Instance.SetUserProfileID(new DuckyID().Value());
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }
}
