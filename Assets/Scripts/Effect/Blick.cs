using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class Blick : MonoBehaviour
{
    public CompositeDisposable _disposables = new CompositeDisposable();
    public void Initialize()
    {
        Observable
            .Timer(System.TimeSpan.FromSeconds(2.0f))
            .Subscribe(_ => Disable())
            .AddTo(_disposables);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _disposables.Dispose();
    }
}
