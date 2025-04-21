using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public abstract class Effect : MonoBehaviour
{
    public CompositeDisposable _disposables = new CompositeDisposable();

    public void Initialize() { }

    public virtual void Initialize(Vector2 startPosition, Sprite sprite) { }

    protected abstract void Action();

    protected void OnDisable()
    {
        _disposables.Dispose();
    }
}
