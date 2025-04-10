using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayEntryPoint : MonoBehaviour
{

    public event Action GoTOMainMenuSceneRequested;
    [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

    public void Run(UIMainView uiMainView)
    {
        Debug.LogError("Load Game Scene");

        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiMainView.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToMainMenuButtonClicked += () => { GoTOMainMenuSceneRequested?.Invoke(); };
    }
}
