using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ReplayButton : ButtonsUI
{
    //[SerializeField] private HideUIBeforeLoadScene _hideUIBefore;

    protected override void ButtonClick()
    {
        GameEntryPoint._instance.RestartGameScene();
        //_hideUIBefore.Hide(() =>  GameEntryPoint._instance.RestartGameScene());
    }
}
