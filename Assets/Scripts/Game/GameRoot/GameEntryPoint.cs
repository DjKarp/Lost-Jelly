using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint 
{
    private static GameEntryPoint _instance;
    private Coroutines m_Coroutines;
    private UIMainView m_UIMainView;

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

        if (sceneName == Scenes.GAME)
        {
            m_Coroutines.StartCoroutine(LoadAndStartGame());
            return;
        }

        if (sceneName != Scenes.BOOTSTRAP)
        {
            return;
        }
#endif

        m_Coroutines.StartCoroutine(LoadAndStartGame());
    }

    private IEnumerator LoadAndStartGame()
    {
        m_UIMainView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOTSTRAP);
        yield return LoadScene(Scenes.GAME);

        yield return new WaitForEndOfFrame();
        
        // create DI conteiner
        var sceneEntryPoint = Object.FindObjectOfType<GameplayEntryPoint>();
        sceneEntryPoint.Run();

        m_UIMainView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
