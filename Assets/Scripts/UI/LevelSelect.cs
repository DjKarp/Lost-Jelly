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
        var levelsPrefab = Resources.LoadAll<Level>("Levels/");

        for (int i = 0; i < levelsPrefab.Length; i++)
        {
            LevelCard levelCard;
            bool isLevelOpen = i <= _lastOpenlevel || i < 10;
            if (i < _levelCards.Count)
            {
                levelCard = _levelCards[i];
            }
            else
            {
                levelCard = Instantiate(_levelCardPrefab, _rootCreateLevelCard);
                _levelCards.Add(levelCard);
            }

            levelCard.Initialize(i, isLevelOpen, i <= _lastOpenlevel ? saveLoadData.GetStarsCount(i) : 0);
        }
    }
    protected new void OnEnable()
    {
        base.OnEnable();
    }

    protected new void OnDisable()
    {
        base.OnDisable();
    }
}
