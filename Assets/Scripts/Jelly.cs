using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] private List<Sprite> _jellyVarList;

    private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _jellyVarList.AddRange(Resources.LoadAll<Sprite>("Jelly"));
        m_SpriteRenderer.sprite = _jellyVarList[Random.Range(0, _jellyVarList.Count - 1)];
    }
}
