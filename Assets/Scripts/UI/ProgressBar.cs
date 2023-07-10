using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private KillsCounter _killsCounter;
    [SerializeField] private SlicedFilledImage _filledImage;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private GameMessenger _messenger;

    private void Awake()
    {
        _filledImage.fillAmount = 0;
    }

    private void OnEnable()
    {
        _killsCounter.Updated += Fill;
    }

    private void OnDisable()
    {
        _killsCounter.Updated -= Fill;
    }

    private void Start()
    {
        _textMesh.text = $"{_killsCounter.CurrentKills} / {_killsCounter.EndGameKills}";
    }

    private void Fill()
    {
        _filledImage.fillAmount = Mathf.Lerp(0, 1, (float)_killsCounter.CurrentKills / _killsCounter.EndGameKills);

        if (_killsCounter.CurrentKills < _killsCounter.EndGameKills)
        {
            _textMesh.text = $"{_killsCounter.CurrentKills} / {_killsCounter.EndGameKills}";
        }
        else
        {
            _textMesh.text = $"{_killsCounter.EndGameKills} / {_killsCounter.EndGameKills}";
            StartCoroutine(DelayedHide());
        }

        IEnumerator DelayedHide()
        {
            yield return new WaitForSeconds(_messenger.WarnAboutBossDuration);

            gameObject.SetActive(false);
        }
    }
}
