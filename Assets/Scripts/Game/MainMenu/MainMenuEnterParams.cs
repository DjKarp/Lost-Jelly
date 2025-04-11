using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEnterParams
{
    public int NumberLevelFinish { get; }

    public MainMenuEnterParams (int numberLevel)
    {
        NumberLevelFinish = numberLevel;
    }
}
