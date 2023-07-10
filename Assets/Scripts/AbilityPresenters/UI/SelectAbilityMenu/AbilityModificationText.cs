using TMPro;
using UnityEngine;

public class AbilityModificationText : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _upgradeValueText;
    [SerializeField] private Color _selectColor;

    public void Render(string description, string currentValue = "", string upgradeValue = "")
    {
        var color = ColorUtility.ToHtmlStringRGB(_selectColor);
        _descriptionText.text = description;
        _upgradeValueText.text = $"{currentValue} <color=#{color}>{upgradeValue}</color>";

        if (currentValue == "" && upgradeValue == "")
            _upgradeValueText.gameObject.SetActive(false);
    }
}
