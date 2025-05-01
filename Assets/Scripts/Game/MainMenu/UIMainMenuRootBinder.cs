using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using DG.Tweening;

public class UIMainMenuRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubject;

    public void HandleGoToGamePlayButtonClick()
    {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.enabled = true;
            canvasGroup
                .DOFade(0.0f, 0.2f)
                .From(1.0f)
                .OnComplete(() => { canvasGroup.enabled = false; _exitSceneSignalSubject?.OnNext(Unit.Default); });
        }
    }

    public void Bind(Subject<Unit> exitSceneSignalSubject)
    {
        _exitSceneSignalSubject = exitSceneSignalSubject;
    }
}
