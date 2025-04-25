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
    private CompositeDisposable _disposable = new CompositeDisposable();
    public void Initialize(Player player, int allJelly, int[] starsTime, int levelNumber)
    {
        m_Player = player;
        _allJelly = allJelly;
        _starsTime = starsTime;
        _levelNumber = levelNumber;

        player.FinishLevel
            .Subscribe(_ => ActivateFinishScreen(_))
            .AddTo(_disposable);

        for (int i = 1; i <= 3; i++)
            _starsSprite.Add(Resources.Load<Sprite>("stars_revard_" + i));

        EventManager.JellyCount.AddListener(SetJellyCount);

        _nextLevelButton.gameObject.SetActive(false);

        Hide();
    }

    private void SetJellyCount(int jelly)
    {
        _jellyCount = jelly;
    }

    private void ActivateFinishScreen(bool isWinner)
    {
        Show();

        int timer = m_CountTime.GetTimerValue();
        int starsCount = !isWinner ? 0 : timer < _starsTime[0] ? 3 : timer <= _starsTime[1] ? 2 : 1;
        _starsImage.sprite = _starsSprite[starsCount];
        _starsImage.transform.DOScale(_starsImage.transform.localScale.x, 0.5f).From(0.0f).SetEase(Ease.InOutBounce);

        if (isWinner)
        {
            _nextLevelButton.gameObject.SetActive(true);
            
            SaveLoadData saveLoadData = new SaveLoadData(_levelNumber, starsCount);

            _nextLevelButton.Add(() =>
            {
                Hide();
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
    }
}
