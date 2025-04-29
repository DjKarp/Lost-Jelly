using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LanguagesData
{
    public string _languageApiKey;
    public string _languageFriendly;
    public Dictionary<string, string[]> _data;

    public LanguagesData(string friendlyName, string apiLangName)
    {
        _languageFriendly = friendlyName;
        _languageApiKey = apiLangName;

        _data = new Dictionary<string, string[]>();
    }    
}

[System.Serializable]
public class FontDesc
{
    public Font _from;
    public Font _to;
}

[System.Serializable]
public class FontMeshProDesc
{
    public TMP_FontAsset _fromTMPRO;
    public TMP_FontAsset _toTMPRO;
}

[System.Serializable]
public class ReplacementFontDesc
{
    public string _language;
    public List<FontDesc> _replacements;
}

[System.Serializable]
public class ReplacementFontMeshProDesc
{
    public string _languageTMPRO;
    public List<FontMeshProDesc> _replacementsTMPRO;
}

[System.Serializable]
public class TextDataDesc
{
    public Font _key;
    public List<Text> _value;

    public void SetFont(Font f)
    {
        foreach (var text in _value)
            if (text != null)
                text.font = f;
    }

    public void SetDefault()
    {
        foreach (var text in _value)
            if (text != null)
                text.font = _key;
    }
}

[System.Serializable]
public class TextMeshProDataDesc
{
    public TMP_FontAsset _keyTMPRO;
    public List<TextMeshProUGUI> _valueTMPRO;

    public void SetFont(TMPro.TMP_FontAsset f)
    {
        foreach (var text in _valueTMPRO)
            text.font = f;
    }

    public void SetDefault()
    {
        foreach (var text in _valueTMPRO)
            text.font = _keyTMPRO;
    }
}

[System.Serializable]
public class TextDBDescription
{
    [System.Serializable]
    public class Entry
    {
        public string Key;

        [TextArea]
        public string[] Values = new string[0];
    }

    public List<Entry> Entries = new List<Entry>();

    public string Find(string key)
    {
        foreach (var entry in Entries)
        {
            if (entry.Key == key && entry.Values.Length > 0)
            {
                return entry.Values[UnityEngine.Random.Range(0, entry.Values.Count())];
            }
        }
        return string.Empty;
    }

    public string Find(string key, params object[] opts)
    {
        foreach (var entry in Entries)
        {
            if (entry.Key == key && entry.Values.Length > 0)
            {
                return string.Format(entry.Values[UnityEngine.Random.Range(0, entry.Values.Count())], opts);
            }
        }
        return string.Empty;
    }
}