using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameplayButton;

    [SerializeField] private Button _levelSelectButton;
    [SerializeField] private GameObject _levelSelectRootGO;
    private LevelSelect m_LevelSelect;    

    [SerializeField] private Image _fonImg;
    [SerializeField] private Sprite _mainMenuImage;
    [SerializeField] private Sprite _levelSelectImage;

    public void Initialize(bool isLevelSelect = false)
    {
        m_LevelSelect = _levelSelectRootGO.GetComponent<LevelSelect>();
        if (isLevelSelect)
            m_LevelSelect.Initialize(this);

        _levelSelectButton.onClick.AddListener(() =>
        {
            m_LevelSelect.Initialize(this);
            SwitchWindows(true);
        });

        SwitchWindows(isLevelSelect);        
    }

    public void SwitchWindows(bool isLevelSelect = false)
    {
        _startGameplayButton.gameObject.SetActive(!isLevelSelect);
        _levelSelectRootGO.SetActive(isLevelSelect);
        _fonImg.sprite = isLevelSelect ? _levelSelectImage : _mainMenuImage;
        _levelSelectButton.gameObject.SetActive(!isLevelSelect);
    }
}
