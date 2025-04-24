using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseScreenUI : ButtonsUI
{
    [SerializeField] private GameObject _closeScreen;

    public override void Initialization()
    {
        
    }

    protected override void ButtonClick()
    {
        EventManager.GameStartStop(true);
        _closeScreen.SetActive(false);
    }
}
