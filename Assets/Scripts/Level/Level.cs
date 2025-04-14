using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _startPositionTR;
    [SerializeField] private bool _spriteDirectionOnLeft = true;
    
    private int _jellyCount = 0;

    public Transform StartPosition
    {
        get => _startPositionTR;
        set => _startPositionTR ??= value;
    }

    [SerializeField] private List<Jelly> _jellyListOnLevel = new List<Jelly>();

    public List<Jelly> JellyListOnLevel
    {
        get => _jellyListOnLevel;
        set => _jellyListOnLevel ??= value;
    }

    public int JellyCount
    {
        get => _jellyCount;
    }

    public bool IsLeftDirectionStartPoint { get => _spriteDirectionOnLeft; }

    private void Awake()
    {
        _jellyCount = JellyListOnLevel.Count;
    }
    private void Start()
    {
        EventManager.SetJellyCount(_jellyCount);
        EventManager.JellyCatched.AddListener(CatchJelly);
    }


    public void AddedOneJelly(Jelly jelly)
    {
        if (jelly != null && !_jellyListOnLevel.Contains(jelly)) _jellyListOnLevel.Add(jelly);
    }

    public void CatchJelly()
    {
        _jellyCount--;
        EventManager.SetJellyCount(_jellyCount);

        if (_jellyCount <= 0)
        {
            Debug.LogError("Winner");
        }
    }
}