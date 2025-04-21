using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class MovieEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMovieRootBinder _UIMovieRootPrefab;
    [SerializeField] private MovieController _movieControllerPrefab;

    private UIMovieRootBinder _UIMovieRootBinder;
    private Button _nextSceneButton;
    private MainMenuEnterParams _mainMenuEnterParams;
    private GamePlayExitParams _gamePlayExitParams;
    private MovieController _movieController;

    private Subject<Unit> _buttonClickSubject = new Subject<Unit>();
    private Subject<Unit> _goToMainMenuSubject = new Subject<Unit>();
    public CompositeDisposable _disposables = new CompositeDisposable();


    public Observable<GamePlayExitParams> Run(UIMainView uIMainView, MainMenuEnterParams mainMenuEnterParams)
    {
        Debug.LogError("Movie Scene Load");

        _UIMovieRootBinder = Instantiate(_UIMovieRootPrefab);
        uIMainView.AttachSceneUI(_UIMovieRootBinder.gameObject);
        _nextSceneButton = _UIMovieRootBinder.GetComponentInChildren<Button>();
        _nextSceneButton.Add(() =>
        {
            _buttonClickSubject?.OnNext(Unit.Default);
        });

        _movieController = Instantiate(_movieControllerPrefab);
        _goToMainMenuSubject = _movieController.Initialize(_buttonClickSubject);
        _goToMainMenuSubject
            .Subscribe(_ => _UIMovieRootBinder.HandleGoToMainMenuButtonClick())
            .AddTo(_disposables);

        Subject<Unit> exitSignalSubject = new Subject<Unit>();
        _UIMovieRootBinder.Bind(exitSignalSubject);

        _mainMenuEnterParams = mainMenuEnterParams;
        _gamePlayExitParams = new GamePlayExitParams(mainMenuEnterParams);
        var exitToMainMenuSceneParam = exitSignalSubject.Select(_ => _gamePlayExitParams);

        return exitToMainMenuSceneParam;
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
        _nextSceneButton.RemoveAll();
    }
}
