using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : DifferentWindowOnMainMenu
{
    protected new void OnEnable()
    {
        _maxScale = 1.0f;
        base.OnEnable();
        Debug.LogError("Pause menu Open!");
    }
}
