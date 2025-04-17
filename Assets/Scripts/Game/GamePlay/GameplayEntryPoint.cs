using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
    [SerializeField] private InputManager _InputManagerPrefab;

    private GamePlayEnterParams _gamePlayEnterParams;

    private List<Sprite> _jellyImageList = new List<Sprite>();
    private Level _levelPrefab;
    private Player m_Player;    
    private MovementHandler _MovementHandler;
    private UIGameplayRootBinder _UIGameplayRootBinder;

    public Observable<GamePlayExitParams> Run(UIMainView uiMainView, GamePlayEnterParams gamePlayEnterParams)
    {
        _UIGameplayRootBinder = Instantiate(_sceneUIRootPrefab);
        uiMainView.AttachSceneUI(_UIGameplayRootBinder.gameObject);

        var exitSceneSignalSubject = new Subject<Unit>();
        _UIGameplayRootBinder.Bind(exitSceneSignalSubject);

        _gamePlayEnterParams = gamePlayEnterParams;
        Debug.LogError("Load Game Scene + sceneName = " + gamePlayEnterParams.SceneName + " => levelNumber = " + gamePlayEnterParams.LevelNumber);

        var mainMenuEnterParams = new MainMenuEnterParams(gamePlayEnterParams.LevelNumber);
        var gamePlayExitParams = new GamePlayExitParams(mainMenuEnterParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubject.Select(_ => gamePlayExitParams);

        Initialize();

        return exitToMainMenuSceneSignal;
    }

    private void Initialize()
    {
        var inputUI = Instantiate(_InputManagerPrefab);
        inputUI.transform.SetParent(_UIGameplayRootBinder.transform, false);
        inputUI.transform.SetSiblingIndex(0);
        inputUI.SwitchOffPanel(false);

        var pressStart = _UIGameplayRootBinder.GetComponentInChildren<PressAnyKeyToStart>();
        inputUI.SubscribeOnStart(pressStart);              

        if (_jellyImageList.Count == 0)
            _jellyImageList.AddRange(Resources.LoadAll<Sprite>("Jelly"));
        var levelPrefab = Resources.Load<Level>("Levels/Level" + _gamePlayEnterParams.LevelNumber);
        _levelPrefab = Instantiate(levelPrefab);
        foreach (Jelly jelly in _levelPrefab.JellyListOnLevel)
            jelly.Initialize(_jellyImageList);        

        GameObject _playerGO = (GameObject) Resources.Load("Player");
        m_Player = Instantiate(_playerGO, _levelPrefab.StartPosition.position, Quaternion.identity).GetComponent<Player>();        
        _MovementHandler = m_Player.gameObject.GetComponent<MovementHandler>();
        m_Player.Initialize(_levelPrefab.IsLeftDirectionStartPoint, _MovementHandler.NewPositionSubject);
        _MovementHandler.Initialize(inputUI, pressStart, _levelPrefab.IsLeftDirectionStartPoint);
        _levelPrefab.Initialize(m_Player);

        var finishScreen = _UIGameplayRootBinder.GetComponentInChildren<FinishScreen>();
        finishScreen.Initialize(m_Player, _levelPrefab.JellyCount, _levelPrefab._starsTime, _gamePlayEnterParams.LevelNumber);        
    }
}
