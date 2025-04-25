using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseButton : ButtonsUI
{
    [SerializeField] private PauseWindow _pauseScreen;

    public override void Initialization()
    {
    }

    protected override void ButtonClick()
    {
        TweensAnimationHide();
        EventManager.GameStartStop(false);
        _pauseScreen.Show(m_Button);
    }
}
