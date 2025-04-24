using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainView : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreenGO;
    [SerializeField] private Transform _UISceneContainerTR;
    private LoadingScreen _loadingScreen;

    public void Initialization()
    {
        _loadingScreen = _loadingScreenGO.GetComponent<LoadingScreen>();
        _loadingScreen.Initialization();
    }

    public void ShowLoadingScreen(R3.Subject<float> loadingSceneSubject)
    {
        _loadingScreen.ShowLoadingScreen(loadingSceneSubject);
    }

    public void HideLoadingScreen()
    {
        _loadingScreen.HideLoadingScreen();
    }

    public void AttachSceneUI(GameObject sceneUI)
    {
        ClearSceneUI();

        sceneUI.transform.SetParent(_UISceneContainerTR, false);
        sceneUI.gameObject.SetActive(true);
    }

    private void ClearSceneUI()
    {
        int childCount = _UISceneContainerTR.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Destroy(_UISceneContainerTR.GetChild(i).gameObject);
        }
    }
}
