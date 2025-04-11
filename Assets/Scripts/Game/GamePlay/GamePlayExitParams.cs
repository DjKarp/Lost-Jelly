using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayExitParams
{
    public MainMenuEnterParams MainMenuEnterParams { get; }

    public GamePlayExitParams(MainMenuEnterParams mainMenuEnterParams)
    {
        MainMenuEnterParams = mainMenuEnterParams;
    }
}
