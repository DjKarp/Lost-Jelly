using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadData
{
    private int _lastOpenLevel;

    public SaveLoadData()
    {
        _lastOpenLevel = PlayerPrefs.GetInt("LastOpenLevel");
    }

    public SaveLoadData(int newLevel)
    {
        _lastOpenLevel = newLevel;
        SetLastOpenLevel(_lastOpenLevel);
    }

    public SaveLoadData(int level, int starsCount)
    {        
        if (level > _lastOpenLevel)
        {
            _lastOpenLevel = level;
            SetLastOpenLevel(_lastOpenLevel);
            SetStarsCount(_lastOpenLevel, starsCount);
        }
        else if (starsCount > GetStarsCount(level))
        {
            SetStarsCount(level, starsCount);
        }
    }

    public int GetLastOpenLevel()
    {
        return _lastOpenLevel;
    }

    public void SetLastOpenLevel(int newLevel)
    {
        PlayerPrefs.SetInt("LastOpenLevel", newLevel);
    }

    public int GetStarsCount(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level_" + levelNumber + "_stars");
    }

    public void SetStarsCount(int levelNumber, int starsCount)
    {
        PlayerPrefs.SetInt("Level_" + levelNumber + "_stars", starsCount);
    }


    public void SavedOpenLavelRandom()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogError("Save cleared and Create new!");
        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            SetLastOpenLevel(i);
            SetStarsCount(i, Random.Range(0, 3));
        }
    }
}
