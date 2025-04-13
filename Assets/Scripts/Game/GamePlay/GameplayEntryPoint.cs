using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
    private GamePlayEnterParams _gamePlayEnterParams;

    [SerializeField] private List<Sprite> _jellyImageList;

    private Player m_Player;
    [SerializeField] private Level _levelPrefab;

    public Observable<GamePlayExitParams> Run(UIMainView uiMainView, GamePlayEnterParams gamePlayEnterParams)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiMainView.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubject = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubject);

        _gamePlayEnterParams = gamePlayEnterParams;
        Debug.LogError("Load Game Scene + sceneName = " + gamePlayEnterParams.SceneName + " => levelNumber = " + gamePlayEnterParams.LevelNumber);

        var mainMenuEnterParams = new MainMenuEnterParams(1);
        var gamePlayExitParams = new GamePlayExitParams(mainMenuEnterParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubject.Select(_ => gamePlayExitParams);

        return exitToMainMenuSceneSignal;
    }

    private void Awake()
    {
        if (_jellyImageList.Count == 0)
            _jellyImageList.AddRange(Resources.LoadAll<Sprite>("Jelly"));
        //_levelPrefab = Resources.Load("Levels/Level" + _gamePlayEnterParams.LevelNumber) as Level;
        foreach (Jelly jelly in _levelPrefab.JellyListOnLevel)
            jelly.Initialize(_jellyImageList);

        GameObject _playerGO = (GameObject) Resources.Load("Player");
        m_Player = Instantiate(_playerGO, _levelPrefab.StartPosition.position, Quaternion.identity).GetComponent<Player>();
    }
}
