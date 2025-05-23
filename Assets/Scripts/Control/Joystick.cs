using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using R3;

public abstract class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickTapArea;
    [SerializeField] private Image _joystickFon;
    [SerializeField] private Image _joystick;

    [SerializeField] private Color _colorInactiveJoystick;
    [SerializeField] private Color _colorActiveJoystick;

    private Vector2 _joystickFonStartPosition;
    private bool _isActiveJoystick = false;

    protected Vector2 _joystickDirection;

    private Vector2 joystickFonPosition;

    private CompositeDisposable _disposable = new CompositeDisposable();
    public Subject<Unit> _subjectJoystick = new();

    private bool _isActive = true;

    private void Start()
    {
        if (_isActive)
        {
            SwitchJoysticSettings();

            _joystickFonStartPosition = _joystickFon.rectTransform.anchoredPosition;
        }
    }

    private void SwitchJoysticSettings()
    {
        if (_isActiveJoystick)
        {
            _joystick.color = _colorActiveJoystick;
            _joystickFon.color = _colorActiveJoystick;
            _isActiveJoystick = false;
        }
        else
        {
            _joystick.color = _colorInactiveJoystick;
            _joystickFon.color = _colorInactiveJoystick;
            _isActiveJoystick = true;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (_isActive && RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickFon.rectTransform, eventData.position, null, out joystickFonPosition))
        {
            joystickFonPosition.x = (joystickFonPosition.x * 2.0f) / _joystickFon.rectTransform.sizeDelta.x;
            joystickFonPosition.y = (joystickFonPosition.y * 2.0f) / _joystickFon.rectTransform.sizeDelta.y;

            _joystickDirection = new Vector2(joystickFonPosition.x, joystickFonPosition.y);
            _joystickDirection = _joystickDirection.magnitude > 1.0f ? _joystickDirection.normalized : _joystickDirection;

            _joystick.rectTransform.anchoredPosition = new Vector2(_joystickDirection.x * (_joystickFon.rectTransform.sizeDelta.x / 2.0f), _joystickDirection.y * (_joystickFon.rectTransform.sizeDelta.y / 2.0f));

            _subjectJoystick.OnNext(Unit.Default);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isActive)
        {
            SwitchJoysticSettings();

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickTapArea.rectTransform, eventData.position, null, out joystickFonPosition))
            {
                _joystickFon.rectTransform.anchoredPosition = new Vector2(joystickFonPosition.x, joystickFonPosition.y);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isActive)
        {
            _joystickFon.rectTransform.anchoredPosition = _joystickFonStartPosition;

            SwitchJoysticSettings();

            _joystickDirection = Vector2.zero;
            _joystick.rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void HideInputOnDevice()
    {
        _isActive = false;
        _joystickTapArea.gameObject.SetActive(false);
        _joystickFon.gameObject.SetActive(false);
        _joystick.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
