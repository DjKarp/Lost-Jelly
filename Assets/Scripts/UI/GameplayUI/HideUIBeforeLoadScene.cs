using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class HideUIBeforeLoadScene : MonoBehaviour
{
    private Image _image;
    private Color _startColor;
    private Color _hideColor;
    private Sequence _sequence;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _startColor = _hideColor = _image.color;
        _startColor = new Color(_hideColor.r, _hideColor.g, _hideColor.b, 0.0f);
        _image.color = _startColor;
    }

    public void Hide(Action callback)
    {
        _sequence = DOTween.Sequence();
        _sequence
            .Append(_image.DOColor(_hideColor, 0.1f).From(_startColor).OnComplete(() => callback?.Invoke()));
    }

    private void OnDisable()
    {
        _sequence.Kill(true);
    }
}
