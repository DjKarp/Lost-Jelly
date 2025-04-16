using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuExitParams
{
    public SceneEnterParams SceneEnterParams { get; }
    public int LevelNumber { get; }

    public MainMenuExitParams(SceneEnterParams sceneEnterParams, int levelNumber)
    {
        SceneEnterParams = sceneEnterParams;
        LevelNumber = levelNumber;
    }
}
