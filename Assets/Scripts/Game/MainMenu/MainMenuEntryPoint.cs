using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuEntryPoint : MonoBehaviour
{
    public event Action GoToGamePlaySceneRequested;
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    public void Run(UIMainView uiMainView, MainMenuEnterParams mainMenuEnterParams)
    {
        Debug.LogError("Load Main Menu Scene");

        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiMainView.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToGamePlayButtonClicked += () => { GoToGamePlaySceneRequested?.Invoke(); };

        Debug.LogError("Main Menu Entry Point: Run Main Menu scene -> " + mainMenuEnterParams?.NumberLevelFinish);
    }
}
