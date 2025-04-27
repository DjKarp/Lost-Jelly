using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using R3;

public abstract class LevelEffects : MonoBehaviour
{
    public CompositeDisposable _disposables = new CompositeDisposable();

    protected Effect _effectPrefab;
    protected List<Effect> _effectPool = new List<Effect>();
    protected Transform _effectParent;
    protected bool isEffectOn = false;
    protected float _timer;

    public virtual void Initialize(List<Jelly> jellies, Subject<bool> subjectBool)
    {
        _effectPool.Clear();

        // Creating an empty parent so that the Root scenes are not filled with objects
        _effectParent = new GameObject(GetType().Name).transform;
        // Creation At initialization, so as not to do Instantiate during execution
        /*for (int i = 10; i > 0; i--)
            AddedNewLevelEffectsToPool(true);*/

        subjectBool
            .Subscribe(_ => StartStopLevelEffect(_))
            .AddTo(_disposables);

        Observable
            .Interval(System.TimeSpan.FromSeconds(_timer))
            .Where(_ => isEffectOn)
            .Subscribe(_ => StartLevelEffects())
            .AddTo(_disposables);
    }

    public virtual void StartStopLevelEffect(bool isStart)
    {
        isEffectOn = isStart;
    }

    protected abstract void StartLevelEffects();
    protected abstract void StartEffect();
    protected abstract Effect AddedNewLevelEffectsToPool(bool isPrewarm = false);

    protected virtual void InitializeEffect(Effect effect)
    {
        effect.gameObject.SetActive(true);
    }

    protected void OnDisable()
    {
        _disposables.Dispose();
    }

    protected void OnDestroy()
    {
        _disposables.Dispose();
    }
}
