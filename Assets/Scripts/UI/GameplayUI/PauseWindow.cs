using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : DifferentWindowOnMainMenu
{
    protected override void StartEndPositionInitialize()
    {
        _endPosition = new Vector2(0.0f, -Screen.height);
        _startPosition = new Vector2(0.0f, Screen.height / 2.0f);
        _transform.position = _endPosition;
    }

    protected new void OnEnable()
    {
        base.OnEnable();
    }
}
