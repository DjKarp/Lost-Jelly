using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private GameObject _hideGO;
    [SerializeField] private Image _starsImage;
    [SerializeField] private CountTime m_CountTime;

    private List<Sprite> _starsSprite = new List<Sprite>();
    private int _jellyCount = 0;
    private int _allJelly = 0;
    public int[] _starsTime;

    private Player m_Player;
    private CompositeDisposable _disposable = new();
    public void Initialize(Player player, int allJelly, int[] starsTime)
    {
        m_Player = player;
        _allJelly = allJelly;
        _starsTime = starsTime;

        player.FinishLevel
            .Subscribe(_ => ActivateFinishScreen(_))
            .AddTo(_disposable);

        for (int i = 1; i <= 3; i++)
            _starsSprite.Add(Resources.Load("stars_revard_" + i) as Sprite);

        EventManager.JellyCount.AddListener(SetJellyCount);

        _hideGO.SetActive(false);
    }

    private void SetJellyCount(int jelly)
    {
        _jellyCount = jelly;
    }

    private void ActivateFinishScreen(bool isWinner)
    {
        float starsKoef = _allJelly / _jellyCount;
        //_starsImage.sprite = _starsSprite[starsKoef == 1 ? 3 : starsKoef > 0.7f ? 2 : starsKoef > 0.3f ? 1 : 0];

        int timer = m_CountTime.GetTimerValue();
        _starsImage.sprite = _starsSprite[!isWinner ? 0 : timer < _starsTime[0] ? 3 : timer <= _starsTime[1] ? 2 : 1];

        _hideGO.SetActive(isWinner);
    }

    private void OnDisable()
    {
        _disposable.Dispose();
    }
}
