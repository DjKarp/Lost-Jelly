using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R3;

public class LevelSelect : DifferentWindowOnMainMenu
{
    [SerializeField] private Transform _rootCreateLevelCard;
    [SerializeField] private LevelCard _levelCardPrefab;
    private int _lastOpenlevel;
    private List<LevelCard> _levelCards = new List<LevelCard>();

    public new void Initialize()
    {
        base.Initialize();

        SaveLoadData saveLoadData = new SaveLoadData();
        _lastOpenlevel = saveLoadData.GetLastOpenLevel();

        for (int i = 0; i <= _lastOpenlevel; i++)
        {
            LevelCard levelCard;
            if (i < _levelCards.Count)
            {
                levelCard = _levelCards[i];
                levelCard.Initialize(i, i <= _lastOpenlevel, saveLoadData.GetStarsCount(i));
            }
            else
            {
                levelCard = Instantiate(_levelCardPrefab, _rootCreateLevelCard);
                levelCard.Initialize(i, i <= _lastOpenlevel, saveLoadData.GetStarsCount(i));
                _levelCards.Add(levelCard);
            }            
        }
    }
    protected new void OnEnable()
    {
        base.OnEnable();
        Debug.LogError("Level Select Open!");
    }

    protected new void OnDisable()
    {
        base.OnDisable();
    }
}
