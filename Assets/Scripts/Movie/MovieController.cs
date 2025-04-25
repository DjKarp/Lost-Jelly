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

    public Subject<Unit> Initialize(Subject<int> buttonClickSubject)
    {
        _playableDirectorMovie.gameObject.SetActive(false);

        _playableDirectorLogo.gameObject.SetActive(true);
        _playableDirectorLogo.Play();

        isStartMovie = true;

        // The button is full-screen, without a picture. we simulate clicking on the screen
        buttonClickSubject
            .Subscribe(_ => StartMovie(_))
            .AddTo(_disposables);

        Observable
            .EveryUpdate()
            .Where(_ => Input.anyKey)
            .Subscribe(_ => StartMovie(_playableDirectorMovie.gameObject.activeSelf ? 1 : 2))
            .AddTo(_disposables);

        return _goToMainMenuSubject;
    }

    public void StartMovie(int clickCount)
    {
        if (clickCount == 1)
        {
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
