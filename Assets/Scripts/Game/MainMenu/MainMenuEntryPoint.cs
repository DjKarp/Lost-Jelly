using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    private UIMainMenuRootBinder _UIMainMenuRootBinder;
    private GamePlayEnterParams _gamePlayEnterParams;
    private MainMenuExitParams _mainMenuExitParams;
    private MainMenu m_MainMenu;

    public Observable<MainMenuExitParams> Run(UIMainView uiMainView, MainMenuEnterParams mainMenuEnterParams, bool isLevelSelect = false)
    {
        Debug.LogError("Load Main Menu Scene");

        _UIMainMenuRootBinder = Instantiate(_sceneUIRootPrefab);
        uiMainView.AttachSceneUI(_UIMainMenuRootBinder.gameObject);

        var exitSignalSubject = new Subject<Unit>();
        _UIMainMenuRootBinder.Bind(exitSignalSubject);

        int levelNumber = mainMenuEnterParams != null ? mainMenuEnterParams.LevelNumber : new SaveLoadData().GetLastOpenLevel();

        _gamePlayEnterParams = new GamePlayEnterParams(levelNumber);
        _mainMenuExitParams = new MainMenuExitParams(_gamePlayEnterParams);

        var exitToGamePlaySceneSignal = exitSignalSubject.Select(x => _mainMenuExitParams);

        Initialize(isLevelSelect);
        Debug.LogError("Main Menu Entry Point: Run Main Menu scene -> " + levelNumber);

        return exitToGamePlaySceneSignal;
    }

    private void Initialize(bool isLevelSelect = false)
    {
        m_MainMenu = _UIMainMenuRootBinder.gameObject.GetComponent<MainMenu>();
        m_MainMenu.Initialize(isLevelSelect);
    }
}
