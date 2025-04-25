using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseLevelButton : ButtonsUI
{
    public override void Initialization()
    {
    }

    protected override void ButtonClick()
    {
        GameEntryPoint._instance.LoadLevelSelect();
    }
}
