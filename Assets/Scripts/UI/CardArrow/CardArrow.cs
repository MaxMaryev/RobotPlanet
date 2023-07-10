using UnityEngine;
using UnityEngine.Events;

public class CardArrow : MonoBehaviour
{
    [SerializeField] private RectTransform _contentRoot;
    [SerializeField] private RectTransform _iconRoot;
    [SerializeField] private CardArrowView _view;

    private Camera _mainCamera;
    private RectTransform _selfRect;
    private AbilityCardPresneter _cardPresenter;
    private Transform _player;
    private float _canvasScale;
    private Vector2 _position;
    private Vector2 _screenCenter;
    private Vector2 _halfRect;

    public event UnityAction<CardArrow> Destroyed;

    public float Distance { get; private set; }

    private void Start()
    {
        _selfRect = transform as RectTransform;
        _mainCamera = Camera.main;
        _screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        _halfRect = _selfRect.sizeDelta / 2f;
    }

    public void Init(AbilityCardPresneter cardPresenter, Transform player, float canvasScale)
    {
        _cardPresenter = cardPresenter;
        _player = player;
        _canvasScale = canvasScale;

        _view.Render(cardPresenter.Ability.AbilityInfo);

        _cardPresenter.Collected += DestroyCardArrow;
        _cardPresenter.Destroyed += DestroyCardArrow;
    }

    private void DestroyCardArrow(AbilityCardPresneter cardPresenter)
    {
        _cardPresenter.Collected -= DestroyCardArrow;
        cardPresenter.Destroyed -= DestroyCardArrow;
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (_cardPresenter == null)
            return;

        var shift = _cardPresenter.transform.position - _player.transform.position;
        var direction = shift.normalized;
        _position = _screenCenter + new Vector2(direction.x, direction.z) * Screen.width / 2f;

        Distance = shift.magnitude;

        var angle = Vector2.SignedAngle(Vector2.left, _position - _screenCenter);
        _selfRect.rotation = Quaternion.Euler(0, 0, angle);

        var cameraPosition = _mainCamera.WorldToScreenPoint(_cardPresenter.transform.position);
        var insideScreen = cameraPosition.x >= 0 && cameraPosition.x < Screen.width && cameraPosition.y >= 0 && cameraPosition.y < Screen.height;
        _contentRoot.gameObject.SetActive(!insideScreen);

        _position.x = Mathf.Clamp(_position.x, _halfRect.x, (Screen.width - _halfRect.x) * (2f - _canvasScale));
        _position.y = Mathf.Clamp(_position.y, _halfRect.y, (Screen.height - _halfRect.y) * (2f - _canvasScale));

        _selfRect.anchoredPosition = _position;
        _iconRoot.rotation = Quaternion.identity;
    }
}