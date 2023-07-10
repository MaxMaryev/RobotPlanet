using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _damageable;
    [SerializeField] private SlicedFilledImage _filledImage;

    private IDamageable _character => (IDamageable)_damageable;

    private void Awake()
    {
        Validate();
        _filledImage.fillAmount = 1;
    }

    private void OnEnable()
    {
        _character.HealthChanged += SetHealth;
    }

    private void OnDisable()
    {
        _character.HealthChanged -= SetHealth;
    }

    private void SetHealth(float health)
    {
        _filledImage.fillAmount = Mathf.Lerp(0f, 1f, health / _character.MaxHealth);
    }

    private void Validate()
    {
        if (_damageable is IDamageable)
            return;

        Debug.LogError(_damageable.name + " needs to implement " + nameof(IDamageable));
        _damageable = null;
    }
}
