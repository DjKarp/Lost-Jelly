using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;
using DG.Tweening;

public class FinishScreen : DifferentWindowOnMainMenu
{
    [SerializeField] private Image _starsImage;
    [SerializeField] private CountTime m_CountTime;
    [SerializeField] private Button _nextLevelButton;

    private List<Sprite> _starsSprite = new List<Sprite>();
    private int _jellyCount = 0;
    private int _allJelly = 0;
    private int[] _starsTime;
    private int _levelNumber;


    private Player m_Player;
    private Sequence _sequence;
    private CompositeDisposable _disposable = new CompositeDisposable();
    public void Initialize(Player player, int allJelly, int[] starsTime, int levelNumber)
    {
        m_Player = player;
        _allJelly = allJelly;
        _starsTime = starsTime;
        _levelNumber = levelNumber;        

        for (int i = 0; i <= 3; i++)
            _starsSprite.Add(Resources.Load<Sprite>("stars_revard_" + i));

        EventManager.JellyCount.AddListener(SetJellyCount);

        _nextLevelButton.gameObject.SetActive(false);

        Hide();

        player.FinishLevel
            .Subscribe(_ => ActivateFinishScreen(_))
            .AddTo(_disposable);
    }

    protected override void StartEndPositionInitialize()
    {
        _endPosition = new Vector2(0.0f, -Screen.height);
        _startPosition = new Vector2(0.0f, Screen.height / 2.0f);
        _transform.position = _endPosition;
    }

    private void SetJellyCount(int jelly)
    {
        _jellyCount = jelly;
    }

    private void ActivateFinishScreen(bool isWinner)
    {
        int timer = m_CountTime.GetTimerValue();
        int starsCount = !isWinner ? 0 : timer < _starsTime[0] ? 3 : timer <= _starsTime[1] ? 2 : 1;
        _starsImage.sprite = _starsSprite[starsCount];

        _sequence = DOTween.Sequence();

        _sequence
            .Append(Show());        

        _sequence
            .Append(_starsImage.transform.DOScale(1.0f, 0.5f).From(0.0f).SetEase(Ease.OutBounce));

        if (isWinner)
        {
            _nextLevelButton.gameObject.SetActive(true);
            _sequence
                .Append(_nextLevelButton.gameObject.transform.DOScale(1.0f, 0.5f).From(0.0f).SetEase(Ease.OutBounce));
            
            SaveLoadData saveLoadData = new SaveLoadData(_levelNumber, starsCount);

            _nextLevelButton.Add(() =>
            {
                Hide(_nextLevelButton, () => _nextLevelButton.gameObject.SetActive(false));
                saveLoadData.SetLastOpenLevel(++_levelNumber);
                GameEntryPoint._instance.NextLevel(_levelNumber);
            });
        }        
    }

    private new void OnDisable()
    {
        base.OnDisable();

        _disposable.Dispose();
        _nextLevelButton.RemoveAll();
        _sequence.Kill(true);
    }
}
