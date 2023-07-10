using UnityEngine;

public class SuccessPanel : EndPanel
{
    [SerializeField] private Boss _boss;

    private void OnEnable()
    {
        _PanelView.SetActive(false);
        _boss.BossDied += EnablePanel;
    }

    private void OnDisable()
    {
        _boss.BossDied -= EnablePanel;
    }
}
