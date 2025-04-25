using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R3;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField] private Button screenButton;

    public Subject<Unit> OnGameplayStart = new();
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        gameObject.SetActive(true);
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
        OnGameplayStart.OnNext(Unit.Default);
        OnGameplayStart.Dispose();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _disposable.Dispose();
    }
}
