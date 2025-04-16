using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;

public class GameEntryPoint 
{
    public static GameEntryPoint _instance;
    private Coroutines m_Coroutines;
    private UIMainView m_UIMainView;

    private GamePlayEnterParams _lastGamePlayEnterParams;
    private MainMenuEnterParams _lastMainMenuEnterParams;

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
        m_Coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(m_Coroutines.gameObject);

        var prefabUImain = Resources.Load<UIMainView>("UIMain");
        m_UIMainView = Object.Instantiate(prefabUImain);
        Object.DontDestroyOnLoad(m_UIMainView.gameObject);
    }

    private void StartGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        switch(sceneName)
        {
            case Scenes.GAME:
                var enterParams = new GamePlayEnterParams(1);
                m_Coroutines.StartCoroutine(LoadAndStartGame(enterParams));
                return;

            case Scenes.MAIN_MENU:
                m_Coroutines.StartCoroutine(LoadAndStartMainMenu());
                return;

            case Scenes.BOOTSTRAP:
                return;

            default: return;
        }
#endif
    }    

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }

    public void RestartGameScene()
    {
        m_Coroutines.StartCoroutine(LoadAndStartGame(_lastGamePlayEnterParams));
    }

    public void NextLevel(int levelNumber)
    {
        m_Coroutines.StartCoroutine(LoadAndStartGame(new GamePlayEnterParams(levelNumber)));
    }

    private IEnumerator LoadAndStartGame(GamePlayEnterParams gamePlayEnterParams)
    {
        _lastGamePlayEnterParams = gamePlayEnterParams;

        m_UIMainView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOTSTRAP);
        yield return LoadScene(Scenes.GAME);

        var sceneEntryPoint = Object.FindObjectOfType<GameplayEntryPoint>();
        sceneEntryPoint.Run(m_UIMainView, gamePlayEnterParams)
            .Subscribe(gamePlayExitParams =>
        {
            m_Coroutines.StartCoroutine(LoadAndStartMainMenu(gamePlayExitParams.MainMenuEnterParams));
        });

        m_UIMainView.HideLoadingScreen();
    }

    public void LoadLevelSelect()
    {
        m_Coroutines.StartCoroutine(LoadAndStartMainMenu(isLevelSelect: true));
    }

    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams = null, bool isLevelSelect = false)
    {
        m_UIMainView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOTSTRAP);
        yield return LoadScene(Scenes.MAIN_MENU);

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(m_UIMainView, mainMenuEnterParams, isLevelSelect)
            .Subscribe(mainMenuExitParams =>
        {
            var targetSceneName = mainMenuExitParams.SceneEnterParams.SceneName;
            
            switch (targetSceneName)
            {
                case (Scenes.GAME):
                    m_Coroutines.StartCoroutine(LoadAndStartGame(mainMenuExitParams.SceneEnterParams.As<GamePlayEnterParams>()));
                    break;
            }
        });

        

        m_UIMainView.HideLoadingScreen();
    }
}
