using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private JoystickMove m_JoystickMove;
    [SerializeField] private ButtonOnScreen m_ButtonOnScreen;

    private Vector2 _direction;

    private Vector2 _tempInputDirection;
    private Vector2 _tempJoystickDirection;

    private CompositeDisposable _disposable = new CompositeDisposable();
    public Subject<Vector2> SubjectInputManager = new();

    private void Start()
    {
        m_JoystickMove._subjectJoystick
            .Subscribe(_ => OnButtonOrJoystickPressed())
            .AddTo(_disposable);

        m_ButtonOnScreen._subjectButtonOnScreen
            .Subscribe(_ => OnButtonOrJoystickPressed())
            .AddTo(_disposable);

        Observable
            .EveryUpdate()
            .Where(_ => (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f))
            .Subscribe(_ => OnButtonOrJoystickPressed())
            .AddTo(_disposable);
    }

    public Vector2 GetMoveDirection()
    {
        _tempInputDirection = _tempJoystickDirection = Vector2.zero;
        _tempInputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _tempJoystickDirection = m_JoystickMove.GetMoveDirection();

        if (_tempInputDirection == Vector2.zero && _tempJoystickDirection == Vector2.zero)
        {
            _tempJoystickDirection = m_ButtonOnScreen.GetDirection();
            _direction = _tempJoystickDirection.x != 0 || _tempJoystickDirection.y != 0 ? _tempJoystickDirection : _direction;
        }
        else
        {
            _tempInputDirection = _tempJoystickDirection == Vector2.zero ? _tempInputDirection : _tempJoystickDirection;
            _direction = Mathf.Abs(_tempInputDirection.x) > Mathf.Abs(_tempInputDirection.y) ? new Vector2(_tempInputDirection.x > 0 ? 1 : -1, 0) : new Vector2(0, _tempInputDirection.y > 0 ? 1 : -1);
        }

        return _direction;
    }

    private void OnButtonOrJoystickPressed()
    {
        var newDirection = GetMoveDirection();
        _direction = newDirection;
        SubjectInputManager.OnNext(_direction);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }

    public void SubscribeOnStart(PressAnyKeyToStart pressAnyKeyToStart)
    {
        pressAnyKeyToStart.OnGameplayStart
            .Subscribe(_ => SwitchOffPanel(false))
            .AddTo(_disposable);
    }

    public void SwitchOffPanel(bool isShow)
    {
        m_JoystickMove.GetComponent<Image>().raycastTarget = isShow;
    }
}
