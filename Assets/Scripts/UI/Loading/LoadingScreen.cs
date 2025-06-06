using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using R3;

public class LoadingScreen : MonoBehaviour
{
    private LoadingBar _loadingBar;

    private Transform _transform;
    private Vector2 _startPosition;
    private Tween _moweAnimationDO;

    private CompositeDisposable _disposables = new CompositeDisposable();

    public void Initialization()
    {
        _transform = gameObject.transform;
        _startPosition = _transform.position;

        _loadingBar = gameObject.GetComponentInChildren<LoadingBar>();
        _loadingBar.Initialization();
    }

    public void ShowLoadingScreen(Subject<float> loadingSceneSubject)
    {
        loadingSceneSubject
            .Subscribe(_ => _loadingBar.SetImageProgress(_))
            .AddTo(_disposables);

        _moweAnimationDO = _transform.DOMoveY(_startPosition.y, 0.5f).From(_startPosition.y + (Screen.height * 2.0f)).SetEase(Ease.Linear);
    }

    public void HideLoadingScreen()
    {
        _moweAnimationDO = _transform.DOMoveX(_startPosition.x - (Screen.width * 2.0f), 0.25f).From(_startPosition.x).SetEase(Ease.Linear);

        StartCoroutine(HideAnimationCorutine());
    }

    private IEnumerator HideAnimationCorutine()
    {
        while (_moweAnimationDO.active)
        {
            if (Input.anyKeyDown)
                _moweAnimationDO.Complete();
            else
                yield return new WaitForEndOfFrame();
        }

        _loadingBar.SetImageProgress(0.0f);
    }

    private void OnDisable()
    {
        _disposables.Dispose();
    }
}
