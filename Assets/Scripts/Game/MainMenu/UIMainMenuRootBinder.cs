using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class UIMainMenuRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubject;

    public void HandleGoToGamePlayButtonClick()
    {
        _exitSceneSignalSubject?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubject)
    {
        _exitSceneSignalSubject = exitSceneSignalSubject;
    }
}
