using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;

public class UIGameplayRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubject;

    public void HandleGoToMainMenuButtonClick()
    {
        _exitSceneSignalSubject?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubject)
    {
        _exitSceneSignalSubject = exitSceneSignalSubject;
    }
}
