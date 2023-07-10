using UnityEngine;

public class DiedPanel : EndPanel
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _PanelView.SetActive(false);
        _player.PlayerDied += EnablePanel;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= EnablePanel;
    }
}
