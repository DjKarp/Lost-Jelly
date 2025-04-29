using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// A card to create in the level selection menu.
/// </summary>

public class LevelCard : MonoBehaviour
{
    [SerializeField] private Image _fonImageOpen;
    [SerializeField] private Image _fonImageClosed;

    [SerializeField] private Image _starsImage;
    [SerializeField] private List<Sprite> _starsSprite = new List<Sprite>();

    [SerializeField] private Image _firstNumberImage;
    [SerializeField] private Image _secondNumberImage;
    [SerializeField] private List<Sprite> _numberSprite = new List<Sprite>();

    private Button m_Button;
    private Tween _buttonClickEffeck;

    public void Initialize(int levelNumber = 0, bool isOpenLevel = true, int starsLevel = 0)
    {
        _fonImageOpen.gameObject.SetActive(isOpenLevel);
        _fonImageClosed.gameObject.SetActive(!isOpenLevel);

        _starsImage.sprite = _starsSprite[starsLevel];
        SetNumberSprite(levelNumber + 1);

        m_Button = gameObject.GetComponent<Button>();
        m_Button.Add(() =>
        {
            m_Button.gameObject.transform
                .DOShakeRotation(0.2f, strength: 45.0f, vibrato: 3, randomness: 60.0f, fadeOut: true, ShakeRandomnessMode.Harmonic)
                .OnComplete(() => GameEntryPoint._instance.NextLevel(levelNumber));
        });
    }

    List<int> digit = new List<int>();

    private void SetNumberSprite(int number)
    {
        digit.Clear();
        for (int i = 0; i < 2; i++)
        {
            if (number > 0)
            {
                digit.Add(number % 10);
                number /= 10;
            }
        }

        if (digit.Count > 1)
        {
            _firstNumberImage.sprite = _numberSprite[digit[1]];
            _secondNumberImage.sprite = _numberSprite[digit[0]];
        }
        else
        {
            _firstNumberImage.sprite = _numberSprite[0];
            _secondNumberImage.sprite = _numberSprite[digit[0]];
        }
    }

    private void OnDisable()
    {
        _buttonClickEffeck.Kill();
        m_Button.RemoveAll();
    }
}
