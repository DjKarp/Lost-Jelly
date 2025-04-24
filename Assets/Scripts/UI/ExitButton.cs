using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : ButtonsUI
{
    public override void Initialization()
    {
    }

    protected override void ButtonClick()
    {
        Application.Quit();
    }
}