using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : DifferentWindowOnMainMenu
{ 
    
    protected new void OnEnable()
    {
        base.OnEnable();
        Debug.LogError("Settings Open!");

        // Test
        SaveLoadData saveLoadData = new SaveLoadData();
        saveLoadData.SavedOpenLavelRandom();
    }
}
