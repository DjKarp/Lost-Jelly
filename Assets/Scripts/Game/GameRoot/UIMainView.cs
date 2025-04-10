using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainView : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;

    public void ShowLoadingScreen()
    {
        gameObject.SetActive(true);
    }

    public void HideLoadingScreen()
    {
        gameObject.SetActive(false);
    }
}
