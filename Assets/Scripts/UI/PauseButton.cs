using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : ButtonsUI
{
    [SerializeField] private GameObject _pauseScreen;
    protected override void ButtonClick()
    {
        EventManager.GameStartStop(false);
        _pauseScreen.SetActive(true);
    }
}
