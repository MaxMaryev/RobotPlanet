using UnityEngine;
using TMPro;
using System;

[Obsolete("Not used anu more")]
public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private ExperienceCollector _experienceCollector;
    [SerializeField] private SlicedFilledImage _filledImage;  
    [SerializeField] private TextMeshProUGUI _textMesh;

    private void Start()
    {
        _filledImage.fillAmount = 0;
        OnLevelReceived();
    }
    private void OnEnable()
    {
        _experienceCollector.Collected += OnCollected;
        _experienceCollector.LevelReceived += OnLevelReceived;
    }

    private void OnDisable()
    {
        _experienceCollector.Collected -= OnCollected;
        _experienceCollector.LevelReceived -= OnLevelReceived;
    }

    private void OnCollected(int experience)
    {
        _filledImage.fillAmount = Mathf.Lerp(0f, 1f, experience / (float)_experienceCollector.MaxExperience);
    }

    private void OnLevelReceived()
    {
        _textMesh.text = "Level " + _experienceCollector.Level;
    }
}
