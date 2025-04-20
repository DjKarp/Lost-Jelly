using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public abstract class Effect : MonoBehaviour
{
    public CompositeDisposable _disposables = new CompositeDisposable();
    protected float _timer;

    public virtual void Initialize(Vector2 startPosition, Sprite sprite = null)
    {
        Observable
            .Timer(System.TimeSpan.FromSeconds(_timer))
            .Subscribe(_ => Action())
            .AddTo(_disposables);
    }

    protected abstract void Action();

    protected void OnDisable()
    {
        _disposables.Dispose();
    }
}
