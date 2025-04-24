using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : ButtonsUI
{
    public override void Initialization()
    {
    }

    protected override void ButtonClick()
    {
        GameEntryPoint._instance.RestartGameScene();
    }
}
