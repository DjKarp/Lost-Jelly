using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] private Image _fonImage;
    [SerializeField] private Sprite _openlevelSprite;
    [SerializeField] private Sprite _closelevelSprite;

    [SerializeField] private Image _starsImage;
    [SerializeField] private List<Sprite> _starsSprite = new List<Sprite>();

    [SerializeField] private Image _firstNumberImage;
    [SerializeField] private Image _secondNumberImage;
    [SerializeField] private List<Sprite> _numberSprite = new List<Sprite>();

    private Button m_Button;

    public void Initialize(int levelNumber = 0, bool isOpenLevel = true, int starsLevel = 0)
    {
        _fonImage.sprite = isOpenLevel ? _openlevelSprite : _closelevelSprite;
        _starsImage.sprite = _starsSprite[starsLevel];
        SetNumberSprite(levelNumber + 1);

        m_Button = gameObject.GetComponent<Button>();
        m_Button.onClick.AddListener(() =>
        {
            GameEntryPoint._instance.NextLevel(levelNumber);
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
            _firstNumberImage.sprite = _numberSprite[digit[0]];
            digit.Remove(0);
        }
        else
            _firstNumberImage.sprite = _numberSprite[0];
        
        _secondNumberImage.sprite = _numberSprite[digit[0]];
    }
}
