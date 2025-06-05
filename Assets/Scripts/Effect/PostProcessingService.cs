using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class PostProcessingService : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    private ChromaticAberration _chromaticAberration;
    private CompositeDisposable _disposables = new CompositeDisposable();

    private void Awake()
    {
        if (_volume.profile.TryGet(out _chromaticAberration))
        {
            _chromaticAberration.active = false;
            _chromaticAberration.intensity.Override(0.0f);
        }
    }

    public void SetEnabledChromatic(Subject<bool> finishLevel)
    {
        finishLevel
            .Subscribe(_ => EnableChromatic(_))
            .AddTo(_disposables);
    }

    private void EnableChromatic(bool isEnabled)
    {
        if (_chromaticAberration != null)
        {
            _chromaticAberration.active = !isEnabled;

            StartCoroutine(FlickerChromatic(2.0f));
        }
    }

    private IEnumerator FlickerChromatic(float duration)
    {
        while (duration > 0.0f)
        {
            float t = Mathf.Sin(duration * 2.0f * Mathf.PI * 2f) * 0.5f + 0.5f;
            _chromaticAberration.intensity.Override(Mathf.Lerp(0f, 1.0f, t));

            duration -= Time.deltaTime;

            if (duration < 0.5f && _chromaticAberration.intensity.value == 0.0f)
                break;

            yield return null;
        }

        _chromaticAberration.intensity.Override(0.0f);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

}
