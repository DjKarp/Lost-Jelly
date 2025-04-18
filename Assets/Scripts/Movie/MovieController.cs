using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using R3;

public class MovieController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirectorLogo;
    [SerializeField] private PlayableDirector _playableDirectorMovie;

    private CompositeDisposable _disposables = new CompositeDisposable();

    private void Awake()
    {
        _playableDirectorMovie.gameObject.SetActive(false);

        _playableDirectorLogo.Play();

        Observable
            .EveryUpdate()
            .Where(_ => _playableDirectorLogo.state != PlayState.Playing)
            .Subscribe(_ => StartMovie())
            .AddTo(_disposables);
    }

    private void StartMovie()
    {
        _playableDirectorMovie.gameObject.SetActive(true);
        _playableDirectorMovie.Play();
    }
}
