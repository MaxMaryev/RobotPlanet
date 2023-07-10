using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public abstract class EndPanel : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private TextMeshProUGUI _killedEnemies;
    [SerializeField] protected GameObject _PanelView;

    public event UnityAction Opened;

    protected void EnablePanel(string _ = "")
    {
        _PanelView.SetActive(true);
        Time.timeScale = 0;
        _killedEnemies.text = Convert.ToString(_enemySpawner.NumberDefeatedEnemies);
        Opened?.Invoke();
    }
}
