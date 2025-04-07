using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[RequireComponent(typeof (Level))]
[ExecuteInEditMode]
public class FindLevelsItems : MonoBehaviour
{
    [SerializeField] public Level m_Level;

    public void StartFindLevelsItems()
    {
        m_Level = gameObject.GetComponent<Level>();
        if (m_Level != null)
        {
            Jelly[] jellies = gameObject.GetComponentsInChildren<Jelly>();
            if (jellies != null && jellies.Length > 0)
                m_Level.JellyListOnLevel.AddRange(jellies.ToList());

            Transform startPosTR = gameObject.transform.Find("StartPosition");
            m_Level.StartPosition = startPosTR;

            Debug.LogError("Find Start Position => " + (m_Level.StartPosition != null) + "____ Find Jelly = " + m_Level.JellyListOnLevel.Count);
        }
        else
        {
            Debug.LogError("No find Level!");
        }
    }
}

[CustomEditor(typeof(FindLevelsItems))]
class DecalMeshHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Find Levels Items"))
        {
            FindLevelsItems m_FindLevelsItems = (FindLevelsItems)target;
            m_FindLevelsItems.StartFindLevelsItems();
        }
    }
}
#endif