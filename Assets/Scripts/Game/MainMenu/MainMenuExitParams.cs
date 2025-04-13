using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuExitParams
{
    public SceneEnterParams SceneEnterParams { get; }

    public MainMenuExitParams(SceneEnterParams sceneEnterParams)
    {
        SceneEnterParams = sceneEnterParams;
    }
}
