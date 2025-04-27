using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R3;
using DG.Tweening;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField] private Button screenButton;

    public Subject<Unit> OnGameplayStart = new();
    private CompositeDisposable _disposable = new CompositeDisposable();
    private CanvasGroup _canvasGroup;
    private Tween _tween;

    private void Awake()
    {
        gameObject.SetActive(true);

        _canvasGroup = gameObject.GetComponent<CanvasGroup>();

        _tween = _canvasGroup.DOFade(1.0f, 0.2f).From(0.0f);

        screenButton.Add(() => HideStartGameplayScreen());

        Observable
            .EveryUpdate()
            .Where(_ => (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("HorizontalDPAD") != 0.0f || Input.GetAxis("VerticalDPAD") != 0.0f
            || Input.GetAxis("Fire2") != 0.0f || Input.GetAxis("Fire1") != 0.0f || Input.GetAxis("Fire3") != 0.0f || Input.GetAxis("Jump") != 0.0f))
            .Subscribe(_ => HideStartGameplayScreen())
            .AddTo(_disposable);
    }

    private void HideStartGameplayScreen()
    {
        screenButton.RemoveAll();
        OnGameplayStart?.OnNext(Unit.Default);
        _tween = _canvasGroup.DOFade(0.0f, 0.2f).From(1.0f).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        OnGameplayStart.Dispose();
        _disposable.Dispose();
    }
}
