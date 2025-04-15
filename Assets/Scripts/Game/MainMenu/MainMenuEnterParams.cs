using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEnterParams
{
    public int LevelNumber { get; set; }
    public bool _isLevelChoiseOpen = false;

    public MainMenuEnterParams (int numberLevel, bool isLevelChoiseOpen = false)
    {
        LevelNumber = numberLevel;
        _isLevelChoiseOpen = isLevelChoiseOpen;
    }
}
