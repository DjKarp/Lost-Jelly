using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _levelSelectButton;
    [SerializeField] private GameObject _levelSelectRootGO;
    [SerializeField] private LevelSelect m_LevelSelect;
    [SerializeField] private Settings m_Settings;
    [SerializeField] private Title m_Title;

    [SerializeField] private Image _fonImg;
    [SerializeField] private Sprite _mainMenuImage;
    [SerializeField] private Sprite _levelSelectImage;

    public void Initialize(bool isLevelSelect = false)
    {
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
        _levelSelectRootGO.SetActive(isLevelSelect);
        m_Settings.gameObject.SetActive(false);
        _fonImg.sprite = isLevelSelect ? _levelSelectImage : _mainMenuImage;
        _levelSelectButton.gameObject.SetActive(!isLevelSelect);
    }

    public void ShowHideSettings()
    {
        bool isShowedSettings = m_Settings.gameObject.activeSelf;
        m_Settings.gameObject.SetActive(!isShowedSettings);
        _fonImg.sprite = !isShowedSettings ? _levelSelectImage : _mainMenuImage;
    }

    public void ShowHideTitle()
    {
        bool isShowedTitle = m_Title.gameObject.activeSelf;
        m_Title.gameObject.SetActive(!isShowedTitle);
        _fonImg.sprite = !isShowedTitle ? _levelSelectImage : _mainMenuImage;
    }
}
