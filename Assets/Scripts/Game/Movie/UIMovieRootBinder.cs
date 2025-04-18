using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class UIMovieRootBinder : MonoBehaviour
{
    public Subject<Unit> _exitSceneMovieSignalSubject;
    
    public void HandleGoToMainMenuButtonClick()
    {
        _exitSceneMovieSignalSubject?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignslSubject)
    {
        _exitSceneMovieSignalSubject = exitSceneSignslSubject;
    }
}
