using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : ButtonsUI
{
    protected override void ButtonClick()
    {
        Application.Quit();
    }
}