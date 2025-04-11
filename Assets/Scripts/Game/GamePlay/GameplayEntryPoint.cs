using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

    public Observable<GamePlayExitParams> Run(UIMainView uiMainView/*, GamePlayEnterParams gamePlayEnterParams*/)
    {
        Debug.LogError("Load Game Scene");

        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiMainView.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubject = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubject);

        var mainMenuEnterParams = new MainMenuEnterParams(1);
        var gamePlayExitParams = new GamePlayExitParams(mainMenuEnterParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubject.Select(_ => gamePlayExitParams);

        return exitToMainMenuSceneSignal;
    }
}
