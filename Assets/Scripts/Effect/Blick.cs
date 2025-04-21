using UnityEngine;
using R3;
public class Blick : Effect
{
    protected float _timer;

    public new void Initialize()
    {
        _timer = 2.0f;

        Observable
            .Timer(System.TimeSpan.FromSeconds(_timer))
            .Subscribe(_ => Action())
            .AddTo(_disposables);
    }
    protected override void Action()
    {
        gameObject.SetActive(false);
    }
}
