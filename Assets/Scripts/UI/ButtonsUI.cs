using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public abstract class ButtonsUI : MonoBehaviour
{
    protected Button m_Button;
    private Tween _tween;
    protected float _maxScale = 1.0f;

    protected void Awake()
    {
        Initialization();
    }

    protected void Initialization()
    {
        m_Button = gameObject.GetComponent<Button>();
        m_Button.Add(ButtonClick);

        TweensAnimationShow();
    }

    protected abstract void ButtonClick();

    protected void TweensAnimationHide()
    {
        _tween = transform.DOScale(0.0f, 0.2f).From(_maxScale).SetEase(Ease.InBounce);
    }

    protected void TweensAnimationShow()
    {
        _tween = transform.DOScale(_maxScale, 0.2f).From(0.0f).SetEase(Ease.OutBounce);
    }

    protected void TweensAnimationBounce()
    {
        _tween = transform.DOScale(transform.localScale, 0.2f).From(transform.localScale.x * _maxScale).SetEase(Ease.InOutBounce);
    }

    private void OnDisable()
    {
        _tween.Kill(true);
        m_Button.RemoveAll();
    }

    private void OnDestroy()
    {
        _tween.Kill(true);
        m_Button.RemoveAll();
    }
}
