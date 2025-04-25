using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image _fonImage;
    private Transform _fonImageTr;
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _levelSelectButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _titleButton;

    [SerializeField] private LevelSelect m_LevelSelect;
    [SerializeField] private Settings m_Settings;
    [SerializeField] private Title m_Title;

    protected Sequence _tween;

    public void Initialize(bool isLevelSelect = false)
    {
        _fonImageTr = _fonImage.gameObject.transform;
        _tween
            .Append(_fonImageTr.DOMoveX(_fonImageTr.position.x, 0.25f).From(_fonImageTr.position.x + (Screen.width * 2.0f)).SetEase(Ease.Linear))
            .Append(_playGameButton.gameObject.transform.DOScale(1.5f, 0.5f).From(0.0f).SetEase(Ease.OutBounce))
            .Append(_levelSelectButton.gameObject.transform.DOScale(1.5f, 0.5f).From(0.0f).SetEase(Ease.OutBounce))
            .Join(_settingsButton.gameObject.transform.DOScale(1.5f, 0.5f).From(0.0f).SetEase(Ease.OutBounce))
            .Join(_titleButton.gameObject.transform.DOScale(1.5f, 0.5f).From(0.0f).SetEase(Ease.OutBounce));

        m_LevelSelect.Initialize();
        m_Settings.Initialize();
        m_Title.Initialize();

        _levelSelectButton.Add(() =>
        {
            SwitchWindows(true);
        });

        SwitchWindows(isLevelSelect);        
    }

    public void SwitchWindows(bool isLevelSelect = false)
    {
        if (isLevelSelect)
            m_LevelSelect.Show(_levelSelectButton);
        else
            m_LevelSelect.Hide(_levelSelectButton, () => m_LevelSelect.gameObject.SetActive(false));
    }

    public void ShowHideSettings()
    {
        if (!m_Settings.IsHide())
            m_Settings.Hide(_settingsButton, () => m_Settings.gameObject.SetActive(false));
        else
            m_Settings.Show(_settingsButton);
    }

    public void ShowHideTitle()
    {
        if (!m_Title.IsHide())
            m_Title.Hide(_titleButton, () => m_Title.gameObject.SetActive(false));
        else
            m_Title.Show(_titleButton);
    }   

    private void OnDisable()
    {
        _tween.Kill(true);
        _levelSelectButton.RemoveAll();
    }
}
