using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseScreenUI : ButtonsUI
{
    [SerializeField] private PauseWindow _pauseScreen;
    [SerializeField] private Button _pauseButton;

    protected override void ButtonClick()
    {
        EventManager.GameStartStop(true);
        _pauseScreen.Hide(_pauseButton);
    }
}
