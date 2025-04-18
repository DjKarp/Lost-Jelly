using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using R3;
using System;

public class MovieController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirectorLogo;
    [SerializeField] private PlayableDirector _playableDirectorMovie;

    private CompositeDisposable _disposables = new CompositeDisposable();
    private Subject<Unit> _goToMainMenuSubject = new Subject<Unit>();
    private int _state = 0;

    public Subject<Unit> Initialize(Subject<Unit> buttonClickSubject)
    {
        _playableDirectorMovie.gameObject.SetActive(false);

        _playableDirectorLogo.Play();
        _state = 0;

        buttonClickSubject
            .Subscribe(_ => StartMovie())
            .AddTo(_disposables);

        Observable
            .EveryUpdate()
            .Where(_ => _state == 0 && _playableDirectorLogo.state != PlayState.Playing)
            .Subscribe(_ => StartMovie())
            .AddTo(_disposables);

        return _goToMainMenuSubject;
    }

    private void StartMovie()
    {
        if (_state == 1)
        {
            _state++;
            _playableDirectorLogo.Deactivate();
            _playableDirectorMovie.gameObject.SetActive(true);
            _playableDirectorMovie.Play();
        }
        else if (_state == 2)
            GoToMainMenu();
    }

    private void GoToMainMenu()
    {
        _state = 0;
        _goToMainMenuSubject?.OnNext(Unit.Default);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
