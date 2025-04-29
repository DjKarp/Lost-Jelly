using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionBeforeStart : MonoBehaviour
{
    private TMP_Text _TextMeshPro;
    [SerializeField] private Dictionary<int, string> _descLevels = new Dictionary<int, string>()
    {
        { 0, "$Gameplay_StartTip_0" },
        { 4, "$Gameplay_StartTip_4" },
        { 15, "$Gameplay_StartTip_15" }
    };


    private void Awake()
    {
        _TextMeshPro = GetComponent<TMP_Text>();
        int level = GameEntryPoint._instance.GetLevelChoised();
        if (_descLevels.ContainsKey(level))
            _TextMeshPro.text = LocalizeManager.Localize(_descLevels[level]);
        else
            _TextMeshPro.text = "";
    }
}
