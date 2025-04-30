using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using R3;
using System;

/// <summary>
/// There are 2 videos in the game at launch. 
/// The first video is the Red Leg Games logo
/// The second video is a cartoon story about a ship flying with Jelly. Along the way, he had a breakdown and all the Jelly scattered. now the Nimble robot needs to collect them all.
/// </summary>
public class MovieController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirectorLogo;
    [SerializeField] private PlayableDirector _playableDirectorMovie;

    private CompositeDisposable _disposables = new CompositeDisposable();
    private Subject<Unit> _goToMainMenuSubject = new Subject<Unit>();

    public Subject<Unit> Initialize(Subject<int> buttonClickSubject)
    {
        _playableDirectorMovie.Deactivate();

        _playableDirectorLogo.Activate();
        _playableDirectorLogo.Play();

        // The button is full-screen, without a picture. we simulate clicking on the screen
        buttonClickSubject
            .Subscribe(_ => StartMovie(_))
            .AddTo(_disposables);

        Observable
            .EveryUpdate()
            .Where(_ => Input.anyKey)
            .Take(2)
            .Subscribe(_ => StartMovie(_playableDirectorMovie.gameObject.activeSelf ? 2 : 1))
            .AddTo(_disposables);

        return _goToMainMenuSubject;
    }

    public void StartMovie(int clickCount)
    {
        if (clickCount == 1)
        {
            _playableDirectorLogo.Deactivate();
            _playableDirectorMovie.Activate();
            _playableDirectorMovie.Play();
        }
        else
            GoToMainMenu();
    }

    private void GoToMainMenu()
    {
        _disposables.Dispose();
        _playableDirectorMovie.Stop();

        _goToMainMenuSubject?.OnNext(Unit.Default);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
