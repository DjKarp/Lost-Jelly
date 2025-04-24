using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : DifferentWindowOnMainMenu
{
    protected new void OnEnable()
    {
        base.OnEnable();
        Debug.LogError("Title Open!");
    }
}
