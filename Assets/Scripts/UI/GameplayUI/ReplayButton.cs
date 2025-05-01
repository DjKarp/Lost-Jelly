using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ReplayButton : ButtonsUI
{
    private UIGameplayRootBinder _UIGameplayRootBinder;

    protected override void ButtonClick()
    {
        _UIGameplayRootBinder = FindAnyObjectByType<UIGameplayRootBinder>();
        _UIGameplayRootBinder?.HandleRebootGameplayScene();
    }
}
