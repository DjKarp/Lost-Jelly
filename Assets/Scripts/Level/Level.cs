using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using R3;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _startPositionTR;
    [SerializeField] private bool _spriteDirectionOnLeft = true;
    [SerializeField] public int[] _starsTime = new int[2];
    private Player m_Player;
    
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


    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Initialize(Player player)
    {
        _jellyCount = JellyListOnLevel.Count;

        m_Player = player;
        m_Player.ReplaySubjectJellyCatch
            .Subscribe(_ => CatchJelly())
            .AddTo(_disposable);
    }

    private void Start()
    {
        EventManager.SetJellyCount(JellyCount);
    }

    public void CatchJelly()
    {
        _jellyCount--;
        EventManager.SetJellyCount(JellyCount);

        if (_jellyCount <= 0)
        {
            EventManager.LevelClear();
            Debug.LogError("Winner");
        }
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}