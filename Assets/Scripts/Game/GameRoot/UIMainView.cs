using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainView : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Transform _UISceneContainerTR;
    [SerializeField] private CountJelly m_CountJelly;

    public void ShowLoadingScreen()
    {
        _loadingScreen.SetActive(true);
    }

    public void HideLoadingScreen()
    {
        _loadingScreen.SetActive(false);
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
