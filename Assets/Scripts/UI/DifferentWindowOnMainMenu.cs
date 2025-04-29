using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

/// <summary>
/// The parent script for any window in the MainMenu has methods for appearing and disappearing. And everything you need to complete subscriptions correctly.
/// </summary>

public class DifferentWindowOnMainMenu : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;
    protected Transform _transform;
    protected Sequence _tween;
    protected float _maxScale = 1.0f;
    protected Vector2 _endPosition = Vector2.zero;
    protected Vector2 _startPosition = Vector2.zero;

    public void Initialize()
    {
        gameObject.SetActive(true);
        _transform = gameObject.transform;

        _tween = DOTween.Sequence();

        StartEndPositionInitialize();

        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0.0f;        
    }

    protected virtual void StartEndPositionInitialize()
    {
        _endPosition = new Vector2(Screen.width / 2.0f, -Screen.height);
        _startPosition = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        _transform.position = _endPosition;
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
            .Join(_canvasGroup.DOFade(_maxScale, 0.5f).From(0.0f).SetEase(Ease.InExpo));

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
                .Append(gameObject.transform.DOMoveY(_endPosition.y, 0.5f).SetEase(Ease.Linear))
                .Join(_canvasGroup.DOFade(0.0f, 0.5f).From(_maxScale))
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
