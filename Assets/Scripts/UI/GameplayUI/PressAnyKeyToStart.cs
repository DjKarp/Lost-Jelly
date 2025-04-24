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
        screenButton.Add(() => HideStartGameplayScreen());
    }

    private void HideStartGameplayScreen()
    {
        screenButton.RemoveAll();
        OnGameplayStart.OnNext(Unit.Default);
        OnGameplayStart.Dispose();
        gameObject.SetActive(false);
    }
}
