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
    public bool isStartMovie = true;

    private CompositeDisposable _disposables = new CompositeDisposable();
    private Subject<Unit> _goToMainMenuSubject = new Subject<Unit>();

    public Subject<Unit> Initialize(Subject<Unit> buttonClickSubject)
    {
        _playableDirectorMovie.gameObject.SetActive(false);

        _playableDirectorLogo.gameObject.SetActive(true);
        _playableDirectorLogo.Play();

        isStartMovie = true;

        buttonClickSubject
            .Subscribe(_ => StartMovie())
            .AddTo(_disposables);

        return _goToMainMenuSubject;
    }

    public void StartMovie()
    {
        if (isStartMovie)
        {
            isStartMovie = false;
            _playableDirectorLogo.Deactivate();
            _playableDirectorMovie.gameObject.SetActive(true);
            _playableDirectorMovie.Play();
        }
        else
            GoToMainMenu();
    }

    private void GoToMainMenu()
    {
        _goToMainMenuSubject?.OnNext(Unit.Default);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
