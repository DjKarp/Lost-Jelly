using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageLocalization : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;

    private void OnEnable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].Activate();
            _buttons[i].Add(() => SetLanguage(i));
        }
        _buttons[LocalizeManager.Instance.CurrentLanguageIndex].Deactivate();
    }

    private void SetLanguage(int locIndex)
    {
        LocalizeManager.Instance.ChangeLanguage(locIndex);
    }
}
