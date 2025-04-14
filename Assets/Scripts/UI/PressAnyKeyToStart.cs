using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R3;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField] private Button screenButton;

    public Subject<Unit> OnGameplayStart = new();

    private void Awake()
    {
        gameObject.SetActive(true);
        screenButton.onClick.AddListener(() => HideStartGameplayScreen());
    }

    private void HideStartGameplayScreen()
    {
        screenButton.RemoveAll();
        OnGameplayStart.OnNext(Unit.Default);
        gameObject.SetActive(false);
    }
}
