using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEnterParams
{
    public int LevelNumber { get; }

    public MainMenuEnterParams (int numberLevel)
    {
        LevelNumber = numberLevel;
    }
}
