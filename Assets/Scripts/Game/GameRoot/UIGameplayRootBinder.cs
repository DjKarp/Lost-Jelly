using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;
using DG.Tweening;

public class UIGameplayRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubject;
    private CanvasGroup canvasGroup;

    public void HandleRebootGameplayScene()
    {
        GoToNewScene(() => GameEntryPoint._instance.RestartGameScene());
    }

    public void HandleGoToMainMenuButtonClick()
    {
        GoToNewScene(() =>_exitSceneSignalSubject?.OnNext(Unit.Default));
    }

    private void GoToNewScene(TweenCallback tweenCallback)
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.enabled = true;
            canvasGroup
                .DOFade(0.0f, 0.2f)
                .From(1.0f)
                .OnComplete(tweenCallback);
        }
    }

    public void Bind(Subject<Unit> exitSceneSignalSubject)
    {
        _exitSceneSignalSubject = exitSceneSignalSubject;
    }
}
