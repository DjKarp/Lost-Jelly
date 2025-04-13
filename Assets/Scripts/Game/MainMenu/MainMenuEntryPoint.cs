using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    public Observable<MainMenuExitParams> Run(UIMainView uiMainView, MainMenuEnterParams mainMenuEnterParams)
    {
        Debug.LogError("Load Main Menu Scene");

        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiMainView.AttachSceneUI(uiScene.gameObject);

        var exitSignalSubject = new Subject<Unit>();
        uiScene.Bind(exitSignalSubject);

        var gameplayEnterParams = new GamePlayEnterParams(mainMenuEnterParams.NumberLevelFinish);
        var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);

        var exitToGamePlaySceneSignal = exitSignalSubject.Select(x => mainMenuExitParams);


        Debug.LogError("Main Menu Entry Point: Run Main Menu scene -> " + mainMenuEnterParams?.NumberLevelFinish);

        return exitToGamePlaySceneSignal;
    }
}
