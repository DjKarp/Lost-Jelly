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
            _buttons[i].enabled = true;
            int index = i;
            _buttons[i].Add(() => SetLanguage(index));
        }
        _buttons[LocalizeManager.Instance.CurrentLanguageIndex].enabled = false;
    }

    private void SetLanguage(int locIndex)
    {
        LocalizeManager.Instance.ChangeLanguage(locIndex);
        OnEnable();
    }
}
