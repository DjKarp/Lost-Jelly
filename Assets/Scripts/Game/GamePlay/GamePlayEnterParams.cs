using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayEnterParams : SceneEnterParams
{
    public int LevelNumber { get; }
    public GamePlayEnterParams(int levelNumber) : base(Scenes.GAME)
    {
        LevelNumber = levelNumber;
    }
}
