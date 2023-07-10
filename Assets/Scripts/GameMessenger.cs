using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameMessenger : MonoBehaviour
{
    const string BossArrived = "Wow.\nThe boss has arrived";
    const string KillTheBoss = "Kill The Boss!";

    [SerializeField] private TextMeshProUGUI _centerMessage;
    [SerializeField] private TextMeshProUGUI _upMessage;
    [SerializeField] private EnemySpawner _spawner;

    private Coroutine _hidingWarnAboutBoss;

    [field: SerializeField] public float WarnAboutBossDuration { get; private set; }

    private void Awake()
    {
        _centerMessage.text = null;
        _upMessage.text = null;
    }

    private void OnEnable()
    {
        _spawner.BossArrived += WarnAboutBoss;
        _spawner.BossArrived += ShowFinalGoal;
    }

    private void OnDisable()
    {
        _spawner.BossArrived -= WarnAboutBoss;
        _spawner.BossArrived -= ShowFinalGoal;
    }

    private void WarnAboutBoss()
    {
        int loopsCount = 4;

        DisplayMessege(_centerMessage, BossArrived);
        PulseText(_centerMessage, loopsCount);

        _hidingWarnAboutBoss = StartCoroutine(DelayedHidingMessege(_centerMessage, WarnAboutBossDuration));
    }

    private IEnumerator DelayedHidingMessege(TextMeshProUGUI typeText, float delay)
    {
        yield return new WaitForSeconds(delay);
        _centerMessage.text = null;
    }

    private void DisplayMessege(TextMeshProUGUI typeMessage, string text)
    {
        typeMessage.text = text;
    }

    private void PulseText(TextMeshProUGUI typeText, int loopsCount)
    {
        Sequence sequence = DOTween.Sequence()
            .Append(typeText.transform.DOScale(1.1f, 0.25f))
            .Append(typeText.transform.DOScale(1f, 0.25f))
            .SetLoops(loopsCount);
    }

    private void ShowFinalGoal()
    {
        StartCoroutine(DelayedShow());

        IEnumerator DelayedShow()
        {
            yield return _hidingWarnAboutBoss;
            DisplayMessege(_upMessage, KillTheBoss);
            PulseText(_upMessage, -1);
        }
    }
}
