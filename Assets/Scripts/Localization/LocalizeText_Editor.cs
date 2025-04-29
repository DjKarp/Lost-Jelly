using System.Linq;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(LocalizeText))]
[CanEditMultipleObjects]
public class LocalizeText_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        foreach (var t in targets)
            UpdateData(t as LocalizeText);
    }

    void UpdateData(LocalizeText localizeText)
    {
        if (localizeText == null)
            return;

        if (localizeText._localizedElements == null || localizeText._localizedElements.Length == 0 || localizeText._localizedElements.Any(t => t._TextMeshPro == null))
        {
            var text = localizeText.GetComponent<Text>();
            var textMeshPro = localizeText.GetComponent<TMPro.TextMeshProUGUI>();
            if (text != null || textMeshPro != null)
            {
                string tag = "";
                if (localizeText.transform.parent != null)
                    tag = $"${localizeText.gameObject.scene.name}_{localizeText.transform.parent.name}_{localizeText.gameObject.name}".Replace(" ", "");
                else
                    tag = $"${localizeText.gameObject.scene.name}_{localizeText.gameObject.name}".Replace(" ", "");

                localizeText._localizedElements = new LocalizeText.LocalizedElement[1] { new LocalizeText.LocalizedElement { _TextMeshPro = textMeshPro, _tag = tag } };
                EditorUtility.SetDirty(localizeText.gameObject);
            }
        }
    }
}
#endif

