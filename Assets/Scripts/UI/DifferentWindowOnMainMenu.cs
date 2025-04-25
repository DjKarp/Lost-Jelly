using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class DifferentWindowOnMainMenu : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;
    protected Transform _transform;
    protected Vector2 _startPosition = Vector2.zero;
    protected Sequence _tween;
    protected Vector2 _endPosition;
    protected float _maxScale = 1.5f;

    public void Initialize()
    {
        gameObject.SetActive(true);

        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0.0f;

        if (_transform == null)
        {
            _transform = gameObject.transform;
            _startPosition = _endPosition = _transform.position;
            _endPosition.y = _startPosition.y - Screen.height;
            _transform.position = _endPosition;
        }
    }

    public Sequence Show(Button button = null)
    {
        gameObject.SetActive(true);

        KillCurrentDOTweens();

        if (button != null)
            _tween
                .Append(button.gameObject.transform.DOScale(0.0f, 0.2f).From(_maxScale).SetEase(Ease.InBounce));

        _tween
            .Append(gameObject.transform.DOMoveY(_startPosition.y, 0.5f).From(_endPosition.y).SetEase(Ease.Linear))
            .Join(_canvasGroup.DOFade(1.0f, 0.5f).From(0.0f).SetEase(Ease.InExpo));

        return _tween;
    }

    public void Hide(Button button = null, Action callback = null)
    {
        if (_canvasGroup.alpha > 0.0f)
        {
            KillCurrentDOTweens();

            if (button != null)
                _tween
                    .Append(button.gameObject.transform.DOScale(_maxScale, 0.2f).From(0.0f).SetEase(Ease.OutBounce));

            _tween
                .Append(gameObject.transform.DOMoveY(_endPosition.y, 0.5f).From(_startPosition.y).SetEase(Ease.Linear))
                .Join(_canvasGroup.DOFade(0.0f, 0.5f).From(1.0f))
                .OnComplete(() => callback?.Invoke());
        }
    }

    public bool IsHide()
    {
        return !gameObject.activeSelf || _canvasGroup.alpha < 1.0f;
    }

    public bool IsAnimationDOTween() => _tween != null && _tween.active;

    protected void KillCurrentDOTweens()
    {
        if (IsAnimationDOTween())
            _tween.Kill();
        _tween = DOTween.Sequence();
    }

    protected void OnEnable()
    {
        Initialize();
    }

    protected void OnDisable()
    {
        _tween.Kill(true);
    }
}
