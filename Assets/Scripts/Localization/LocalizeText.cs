using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class LocalizeText : MonoBehaviour
{
    public static List<LocalizeText> ActiveTexts = new List<LocalizeText>();

    public LocalizedElement[] _localizedElements;

    public void UpdateLocalization()
    {
        foreach (var locElement in _localizedElements)
            ApplyLocalization(locElement);
    }

    public void ApplyLocalization(LocalizedElement locElement)
    {
        var localized = LocalizeManager.Localize(locElement._tag);
        if (locElement._TextMeshPro != null)
            locElement._TextMeshPro.text = localized;
    }

    private void OnEnable()
    {
        ActiveTexts.Add(this);
        UpdateLocalization();
    }

    private void OnDisable()
    {
        ActiveTexts.Remove(this);
    }
    [System.Serializable]
    public class LocalizedElement
    {
        public string _tag;
        public string _defaultText;

        public TextMeshProUGUI _TextMeshPro;
    }
}
