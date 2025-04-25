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
    private FinishLevel m_FinishLevel;
    private Player m_Player;
    private FlyLeaves _FlyLeaves;
    private Blicker _Blicker;
    private Shaker _Shaker;
    
    private int _jellyCount = 0;

    private Subject<bool> playGameSubject = new Subject<bool>();

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
        m_FinishLevel = gameObject.GetComponentInChildren<FinishLevel>();
        m_FinishLevel.gameObject.SetActive(false);

        _jellyCount = JellyListOnLevel.Count;

        EventManager.PlayerMove.AddListener(StartStopGameplay);
        EventManager.AllJellyCatched.AddListener(Finish);

        m_Player = player;
        m_Player.ReplaySubjectJellyCatch
            .Subscribe(_ => CatchJelly())
            .AddTo(_disposable);

        // Effect on Level
        _FlyLeaves = new FlyLeaves();
        _FlyLeaves.Initialize(null, playGameSubject);

        // Effect on Jelly
        _Blicker = new Blicker();
        _Blicker.Initialize(JellyListOnLevel, playGameSubject);
        _Shaker = new Shaker();
        _Shaker.Initialize(JellyListOnLevel, playGameSubject);
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

    private void StartStopGameplay(bool isStart)
    {
        playGameSubject?.OnNext(isStart);
    }

    private void Finish()
    {
        m_FinishLevel.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
        EventManager.PlayerMove.RemoveListener(StartStopGameplay);
        EventManager.AllJellyCatched.RemoveListener(Finish);
    }
}