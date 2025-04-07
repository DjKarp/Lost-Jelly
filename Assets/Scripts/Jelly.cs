using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] private List<Sprite> jellyVarList;

    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = jellyVarList[Random.Range(0, jellyVarList.Count - 1)];
    }
}
