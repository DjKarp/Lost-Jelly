using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;

/// <summary>
/// A common entry point to the project.
/// </summary> 
public class GameEntryPoint 
{
    public static GameEntryPoint _instance;
    private Coroutines _Coroutines;
    private UIMainView _UIMainView;
    private AudioManager _AudioManager;
    private SaveLoadData _SaveLoadData;
    private LocalizeManager _LocalizeManager;

    private GamePlayEnterParams _lastGamePlayEnterParams;
    private Subject<float> _loadSceneSubject = new Subject<float>();

    private int _level;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutoStartGame()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        _instance = new GameEntryPoint();
        _instance.StartGame();
    }

    private GameEntryPoint()
    {
        _SaveLoadData = new SaveLoadData();
        _level = _SaveLoadData.GetLastOpenLevel();

        var prefabAudioManager = Resources.Load<AudioManager>("AudioManager");
        _AudioManager = Object.Instantiate(prefabAudioManager);
        Object.DontDestroyOnLoad(_AudioManager.gameObject);
        _AudioManager.Initialization(_SaveLoadData);

        var localizeManager = Resources.Load<LocalizeManager>("LocalizeManager");
        _LocalizeManager = Object.Instantiate(localizeManager);
        Object.DontDestroyOnLoad(_LocalizeManager);
        _LocalizeManager.Init();

        _Coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_Coroutines.gameObject);

        var prefabUImain = Resources.Load<UIMainView>("UIMain");
        _UIMainView = Object.Instantiate(prefabUImain);
        Object.DontDestroyOnLoad(_UIMainView.gameObject);
        _UIMainView.Initialization();
    }

    private void StartGame()
    {
//#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        switch(sceneName)
        {
            case Scenes.GAME:
                var enterParams = new GamePlayEnterParams(_level);
                _Coroutines.StartCoroutine(LoadAndStartGame(enterParams));
                return;

            case Scenes.BOOTSTRAP:
            case Scenes.MOVIE:
                _Coroutines.StartCoroutine(LoadAndStartMovie());
                return;

            //case Scenes.BOOTSTRAP:                                  // Added for Start from Boostrap
            case Scenes.MAIN_MENU:
                _Coroutines.StartCoroutine(LoadAndStartMainMenu());
                return;
                /*
            case Scenes.BOOTSTRAP:
                return;*/

            default: return;
        }
//#endif
    }    

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation LoadAsync =  SceneManager.LoadSceneAsync(sceneName);
        
        while(!LoadAsync.isDone && LoadAsync.progress < 0.5f)
        {
            _loadSceneSubject?.OnNext(LoadAsync.progress);
        }

        yield return LoadAsync;
    }

    public void RestartGameScene()
    {
        _Coroutines.StartCoroutine(LoadAndStartGame(_lastGamePlayEnterParams));
    }

    public void NextLevel(int levelNumber)
    {
        _Coroutines.StartCoroutine(LoadAndStartGame(new GamePlayEnterParams(levelNumber)));
    }

    private IEnumerator LoadAndStartGame(GamePlayEnterParams gamePlayEnterParams)
    {
        _UIMainView.ShowLoadingScreen(_loadSceneSubject);
        _lastGamePlayEnterParams = gamePlayEnterParams;
        _level = gamePlayEnterParams.LevelNumber;

        yield return LoadScene(Scenes.BOOTSTRAP);
        yield return new WaitForSeconds(0.5f);
        yield return LoadScene(Scenes.GAME);


        var sceneEntryPoint = Object.FindObjectOfType<GameplayEntryPoint>();
        sceneEntryPoint.Run(_UIMainView, gamePlayEnterParams)
            .Subscribe(gamePlayExitParams =>
        {
            _Coroutines.StartCoroutine(LoadAndStartMainMenu(gamePlayExitParams.MainMenuEnterParams));
        });

        _UIMainView.HideLoadingScreen();
    }

    public void LoadLevelSelect()
    {
        _Coroutines.StartCoroutine(LoadAndStartMainMenu(isLevelSelect: true));
    }

    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams = null, bool isLevelSelect = false)
    {
        _UIMainView.ShowLoadingScreen(_loadSceneSubject);

        yield return LoadScene(Scenes.BOOTSTRAP);
        yield return new WaitForSeconds(0.5f);
        yield return LoadScene(Scenes.MAIN_MENU);


        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(_UIMainView, mainMenuEnterParams, isLevelSelect, _SaveLoadData)
            .Subscribe(mainMenuExitParams =>
        {
            var targetSceneName = mainMenuExitParams.SceneEnterParams.SceneName;
            
            switch (targetSceneName)
            {
                case (Scenes.GAME):
                    _Coroutines.StartCoroutine(LoadAndStartGame(mainMenuExitParams.SceneEnterParams.As<GamePlayEnterParams>()));
                    break;
            }
        });

        

        _UIMainView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMovie(MainMenuEnterParams mainMenuEnterParams = null)
    {
        _UIMainView.ShowLoadingScreen(_loadSceneSubject);

        yield return LoadScene(Scenes.BOOTSTRAP);
        yield return new WaitForSeconds(0.5f);
        yield return LoadScene(Scenes.MOVIE);
        MovieEntryPoint sceneMovieEntryPoint = Object.FindObjectOfType<MovieEntryPoint>();

        sceneMovieEntryPoint.Run(_UIMainView, mainMenuEnterParams)
            .Subscribe(gameplayExitParams =>
            {
                _Coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
            });

        _UIMainView.HideLoadingScreen();
    }

    public int GetLevelChoised()
    {
        return _level;
    }
}
