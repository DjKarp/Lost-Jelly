using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LocalizeManager : MonoBehaviour
{
    private static LocalizeManager _instance;
    public static LocalizeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = Resources.Load<GameObject>("LocalizeManager");
                _instance = go.GetComponent<LocalizeManager>();
                _instance.Init();
            }

            return _instance;
        }
    }

    public List<string> _languagesList;
    public LanguagesData _language;
    public LanguagesData _defaultLanguage;

    public string CurrentLanguage = "English";
    public static readonly string DefaultLanguageName = "English";
    public int CurrentLanguageIndex = 0;

    private SaveLoadData _saveLoadData;

    public static string[] OfficialSupportedLanguages = new string[]
    {
        "English",
        "Russian",
        /*"German",
        "French",
        "Spanish",
        "Japanese",
        "Chinese",
        "Korean"  */      
    };

    private bool debugMode = true;

    public void Init()
    {
        ReadLanguagesList();

        _saveLoadData = new SaveLoadData();
        CurrentLanguageIndex = _saveLoadData.GetLanguageIndex();
        CurrentLanguage = _languagesList[CurrentLanguageIndex];
        ReadCurrentLanguage();        
    }


    void ReadLanguagesList()
    {
        _languagesList = new List<string>();

        var languages = Directory.GetFiles(Path.Combine(Application.streamingAssetsPath, "Languages"), "*.lng");
        var sortedLanguages = languages
            .OrderBy(x => 
            {
                var index = System.Array.IndexOf(OfficialSupportedLanguages, Path.GetFileNameWithoutExtension(x));
                return index < 0 ? int.MaxValue : index;
            }).ToArray();

        for (int i = 0; i < sortedLanguages.Length; i++)
        {
            var friendlyLangName = Path.GetFileNameWithoutExtension(sortedLanguages[i]);
            _languagesList.Add(friendlyLangName);
        }
    }

    void ReadCurrentLanguage()
    {
        if (_language != null && _language._languageFriendly == CurrentLanguage)
            return;

        if (_languagesList == null)
            ReadLanguagesList();

        _language = LoadLanguage(CurrentLanguage);
        _defaultLanguage = LoadLanguage(DefaultLanguageName);
    }

    private static LanguagesData LoadLanguage(string currentLanguage)
    {
        Dictionary<string, (int, int)> sameCount = new Dictionary<string, (int, int)>();
        List<(int, int)> amounts = new List<(int, int)>();

        for (int i = 0; i < 5; i++)
            amounts.Add((0, 0));

        LanguagesData language = null;

        string filePath = Path.Combine(Application.streamingAssetsPath, "Languages", currentLanguage + ".lng");
        if (File.Exists(filePath))
        {
            language = new LanguagesData(currentLanguage, "");
            var languageData = File.ReadAllLines(filePath);

            int wordsCount = 0;
            bool count = false;
            foreach (var d in languageData)
            {
                string[] opts = d.Split(';');
                string key = opts[0];
                string[] langData = opts.Skip(1).Select(t => t.Replace("\\n", "\n").Replace("◙", ";").Replace("\\t", "\t")).ToArray();

                if (count)
                {
                    foreach (var v in langData)
                    {
                        int _wordsCount = 0;
                        if (!string.IsNullOrEmpty(v))
                        {
                            var opt = v.Split(' ');
                            foreach (var o in opt)
                            {
                                if (!string.IsNullOrEmpty(o) && o.Length > 1 && !o.Contains(">") && !o.Contains("{") && !o.Contains("["))
                                    _wordsCount++;
                            }

                            if (count)
                            {
                                if (_wordsCount >= 20)
                                {
                                    amounts[0] = (amounts[0].Item1 + 1, amounts[0].Item2 + _wordsCount);
                                }
                                else if (_wordsCount >= 10)
                                {
                                    amounts[1] = (amounts[1].Item1 + 1, amounts[1].Item2 + _wordsCount);
                                }
                                else if (_wordsCount >= 5)
                                {
                                    amounts[2] = (amounts[2].Item1 + 1, amounts[2].Item2 + _wordsCount);
                                }
                                else if (_wordsCount >= 3)
                                {
                                    amounts[3] = (amounts[3].Item1 + 1, amounts[3].Item2 + _wordsCount);
                                }
                                else if (_wordsCount >= 1)
                                {
                                    amounts[4] = (amounts[4].Item1 + 1, amounts[4].Item2 + _wordsCount);
                                }
                            }

                            wordsCount += _wordsCount;
                        }

                        if (count)
                        {
                            if (!sameCount.ContainsKey(v))
                            {
                                sameCount.Add(v, (0, 0));
                            }

                            sameCount[v] = (sameCount[v].Item1 + 1, sameCount[v].Item2 + _wordsCount);
                        }
                    }
                }

                if (language._data.ContainsKey(key))
                {
                    continue;
                }
                language._data.Add(key, langData);
            }

            if (count)
            {
                string result = $"Total words count: {wordsCount}\n";
                for (int i = 0; i < 5; i++)
                {
                    string words = "";
                    switch (i)
                    {
                        case 0:
                            words = "More than 20 words: ";
                            break;
                        case 1:
                            words = "10-20 words: ";
                            break;
                        case 2:
                            words = "5-9 words: ";
                            break;
                        case 3:
                            words = " 3-4 words: ";
                            break;
                        case 4:
                            words = "1-2 words: ";
                            break;
                    }

                    result += $"{words} {amounts[i].Item1} blocks, words: {amounts[i].Item2}\n";
                }

                Debug.LogError(result);

                int sameCountAmount = sameCount.Where(x => x.Value.Item1 > 1).Sum(x => x.Value.Item2);
                int sameCountAmount2 = sameCount.Where(x => x.Value.Item1 > 1).Sum(x => x.Value.Item1);
                Debug.LogError(sameCountAmount2 + ": " + sameCountAmount);
            }
        }

        return language;
    }

    public void ChangeLanguage(int langIdx)
    {
        if (langIdx > _languagesList.Count)
            langIdx = 0;

        CurrentLanguageIndex = langIdx;
        CurrentLanguage = _languagesList[CurrentLanguageIndex];

        ReadCurrentLanguage();

        foreach (var l in LocalizeText.ActiveTexts)
        {
            l.UpdateLocalization();
        }

        _saveLoadData.SetLanguageindex(langIdx);
    }

    // LocalizeManager.Localize("$Ui_")
    public List<string> Localize<T>(string tagPart, bool toUpper = false)
    {
        return System.Enum.GetNames(typeof(T)).Select(x => Localize(string.Concat(tagPart, toUpper ? x.ToUpper() : x))).ToList();
    }

    public static List<string> Localize(IEnumerable<string> tags)
    {
        return tags.Select(x => Localize(x)).ToList();
    }

    public static string Localize(string tag, params object[] p)
    {
        if (string.IsNullOrEmpty(tag) ||
            tag[0] != '$')
        {
            return tag;
        }

        if (tag.Contains(';'))
        {
            string[] tagOpts = tag.Split(';');
            if (tagOpts.Length > 1)
            {
                tag = tagOpts[0];
                var paramsList = tagOpts.ToList();
                paramsList.RemoveAt(0);

                for (int i = 0; i < paramsList.Count; i++)
                {
                    if (paramsList[i].StartsWith("$"))
                    {
                        paramsList[i] = Localize(paramsList[i]);
                    }
                }

                p = paramsList.ToArray();
            }
        }

        string[] opts = Instance.GetTagOptions(tag);

        if (opts != null)
        {
            if (Instance.debugMode)
            {
                try
                {
                    string opt = null;
                    if (opts.Length > 1)
                    {
                        opt = opts[Random.Range(0, opts.Length)];
                    }
                    else
                    {
                        opt = opts[0];
                    }

                    if (p.Length == 0)
                    {
                        return opt;
                    }

                    return string.Format(opt, p);
                }
                catch (System.Exception)
                {
                    Debug.LogError("TAG " + tag + ", opts, " + string.Join(",", p));
                }
            }
            else
            {
                return string.Format(opts[Random.Range(0, opts.Length)], p);
            }
        }

        Debug.LogError(tag);
        return "#" + tag;
    }

    public string[] GetTagOptions(string tag)
    {
        string[] opts;
        if (!_language._data.TryGetValue(tag, out opts))
        {
#if !UNITY_EDITOR
            _defaultLanguage._data.TryGetValue(tag, out opts);
#endif
        }

        return opts;
    }

#if UNITY_EDITOR

    public static Dictionary<string, LanguagesData> EditorLanguages;

    public static void SaveTags()
    {
        var langs = EditorLanguages.Keys.ToList();
        foreach (var l in langs)
        {
            List<string> langDataList = new List<string>();
            string filePath = Path.Combine(Application.streamingAssetsPath, "Languages", l + ".lng");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var langData = EditorLanguages[l];
            foreach (var ld in langData._data)
            {
                string result = ld.Key + ";" + string.Join(";", ld.Value);
                result = result.Replace("\n", "\\n").Replace("\t", "\\t");
                langDataList.Add(result);
            }

            File.WriteAllLines(filePath, langDataList);
        }

        EditorLanguages = null;
    }

    public static void AddTag(string tag, string language, string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            return;
        }

        if (EditorLanguages == null)
        {
            EditorLoadLanguages();
        }

        EditorLanguages[language]._data.Add(tag, new string[1] { val });
    }

    public static string EditorLocalize(string tag, string language, params object[] p)
    {
        if (EditorLanguages == null)
        {
            EditorLoadLanguages();
        }

        if (string.IsNullOrEmpty(tag) ||
            tag[0] != '$')
        {
            return tag;
        }

        if (tag.Contains(';'))
        {
            string[] tagOpts = tag.Split(';');
            if (tagOpts.Length > 1)
            {
                tag = tagOpts[0];
                var paramsList = tagOpts.ToList();
                paramsList.RemoveAt(0);

                for (int i = 0; i < paramsList.Count; i++)
                {
                    if (paramsList[i].StartsWith("$"))
                    {
                        paramsList[i] = EditorLocalize(paramsList[i], language);
                    }
                }

                p = paramsList.ToArray();
            }
        }

        string[] opts;
        EditorLanguages[language]._data.TryGetValue(tag, out opts);

        if (opts != null)
        {
            try
            {
                return string.Format(opts[Random.Range(0, opts.Length)], p);
            }
            catch (System.Exception e)
            {
                Debug.LogError("TAG " + tag + ", opts, " + string.Join(",", p) + " - " + e.Message);
            }
        }

        Debug.LogError("TAG: " + tag + " is null");
        return string.Empty;
    }

    public static void EditorStart()
    {
        EditorLanguages = null;
    }

    private static void EditorLoadLanguages()
    {
        FetchData();

        EditorLanguages = new Dictionary<string, LanguagesData>();
        var languages = Directory.GetFiles(Path.Combine(Application.streamingAssetsPath, "Languages"), "*.lng");
        for (int i = 0; i < languages.Length; i++)
        {
            var friendlyLangName = Path.GetFileNameWithoutExtension(languages[i]);
            LanguagesData data = new LanguagesData(friendlyLangName, "");

            string filePath = Path.Combine(Application.streamingAssetsPath, "Languages", friendlyLangName + ".lng");
            var languageData = File.ReadAllLines(filePath);

            foreach (var l in languageData)
            {
                string[] opts = l.Split(';');

                string key = opts[0];
                string[] langData = opts.Skip(1).Select(t => t.Replace("\\n", "\n").Replace("\\t", "\t")).ToArray();
                data._data.Add(key, langData);
            }

            EditorLanguages.Add(friendlyLangName, data);
        }
    }

    public static string ExportFromGoogleRaw(string documentId, string pageName)
    {
        WWW www = new WWW("https://docs.google.com/spreadsheets/d/" + documentId + "/gviz/tq?tqx=out:csv&charset=utf-8&sheet=" + WWW.EscapeURL(pageName).Replace("+", "%20"));
        while (!www.isDone)
        {
        }

        return www.text;
    }

    public static List<string[]> SpecialParser(string data)
    {
        List<string[]> result = new List<string[]>();
        List<string> rowData = new List<string>();

        bool inQuoute = false;
        int currentQuote = 0;
        string currentData = "";

        for (int i = 0; i < data.Length; i++)
        {
            char current = data[i];
            if (currentQuote == 0)
            {
                if (inQuoute)
                {
                    if (current == ',')
                    {
                        rowData.Add(currentData);

                        inQuoute = false;
                        currentData = "";
                    }
                    else
                    {
                        currentData += current;
                    }
                }
                else
                {
                    if (current == ',')
                    {
                        currentData = "";
                    }
                    else if (current == '"')
                    {
                        currentQuote++;
                    }
                    else
                    {
                        currentData += current;
                        inQuoute = true;
                    }
                }
            }
            else if (currentQuote == 1)
            {
                if (current == '"')
                {
                    currentQuote++;
                }
                else
                {
                    currentData += current;
                }
            }
            else if (currentQuote > 1)
            {
                if (current == '"')
                {
                    currentData += current;
                    currentQuote = 1;
                }
                else if (current == ',')
                {
                    rowData.Add(currentData);

                    currentQuote = 0;
                    currentData = "";
                }
                else if (current == '\n')
                {
                    result.Add(rowData.ToArray());
                    rowData.Clear();
                    currentData = "";
                    currentQuote = 0;
                }
            }
        }

        if (rowData.Count > 0)
        {
            result.Add(rowData.ToArray());
        }

        return result;
    }

    public static List<string[]> ExportFromGoogle(string documentId, string pageName)
    {
        string csv = ExportFromGoogleRaw(documentId, pageName);
        var result = SpecialParser(csv);
        return result;
    }

#if UNITY_EDITOR
    [EditorButton]
#endif
    public static void FetchData()
    {
        if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, "Languages")))
        {
            Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, "Languages"));
        }

        Dictionary<int, List<string[]>> perWordCount = new Dictionary<int, List<string[]>>();

        float idx = 0f;
        foreach (var l in OfficialSupportedLanguages)
        {
            idx += 1;

            bool count = false;
            EditorUtility.DisplayProgressBar("Updating", l, OfficialSupportedLanguages.Length / idx);
            int wordCount = 0;
            List<TextDBDescription.Entry> entires = new List<TextDBDescription.Entry>();
            var entries = ExportFromGoogle("186HGaP3WarD9TXPc7AeHO3YbiBnRSAJu3xWMji8gO00", l).Skip(1).ToList();

            foreach (var e in entries)
            {
                if (e.Length < 3)
                {
                    continue;
                }

                TextDBDescription.Entry entry = new TextDBDescription.Entry { Key = e[0] };
                entry.Values = e.Skip(1).Take(1).Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Replace(";", "◙")).ToArray();

                for (int i = 0; i < entry.Values.Length; i++)
                {
                    entry.Values[i] = entry.Values[i].Replace("\r\n", "\\n").Replace("\n", "\\n").Replace("\t", "\\t");
                }

                if (count)
                {
                    foreach (var v in entry.Values)
                    {
                        if (!string.IsNullOrEmpty(v))
                        {
                            var opts = v.Split(' ');
                            foreach (var o in opts)
                            {
                                if (!string.IsNullOrEmpty(o) &&
                                    o.Length > 1 &&
                                    !o.Contains(">") &&
                                    !o.Contains("{"))
                                {
                                    wordCount++;
                                }
                            }
                        }
                    }
                }

                entires.Add(entry);
            }

            if (count)
            {
                Debug.LogError("C: " + wordCount);
            }

            string filePath = Path.Combine(Application.streamingAssetsPath, "Languages", l + ".lng");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllLines(filePath, ConvertToCsv(entires));
        }

        EditorUtility.ClearProgressBar();
        EditorUtility.DisplayDialog("Success", "Languages Updated", "OK");
    }

    public static string[] ConvertToCsv(List<TextDBDescription.Entry> entries)
    {
        string[] result = new string[entries.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = entries[i].Key + ";" + string.Join(";", entries[i].Values);
        }

        return result;
    }
#endif
        
    void OnDestroy()
    {
        _instance = null;
    }
}
