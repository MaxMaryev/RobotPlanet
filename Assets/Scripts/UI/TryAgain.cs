using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TryAgain : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event UnityAction Reloading;

    private void OnEnable()
    {
        _button.onClick.AddListener(ReloadingScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ReloadingScene);
    }

    public void ReloadingScene()
    {
        Time.timeScale = 1;
        Reloading?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}