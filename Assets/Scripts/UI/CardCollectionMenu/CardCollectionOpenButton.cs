using UnityEngine;
using UnityEngine.UI;

public class CardCollectionOpenButton : MonoBehaviour
{
    [SerializeField] private PickCardCollection _collection;
    [SerializeField] private Abilities _abilities;
    [SerializeField] private Button _openButton;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(OnOpenButtonClicked);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnOpenButtonClicked);
    }

    private void OnOpenButtonClicked()
    {
        _collection.Open(_abilities.SelectedAbilitied);
    }
}
