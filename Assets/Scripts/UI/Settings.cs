using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.LogError("Settings Open!");

        // Test
        SaveLoadData saveLoadData = new SaveLoadData();
        saveLoadData.SavedOpenLavelRandom();
    }
}
