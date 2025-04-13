using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Jelly : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;

    public void Initialize(List<Sprite> _jellyImageList)
    {
        // Меняем внешний вид Jelly на рандоминый
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = _jellyImageList[Random.Range(0, _jellyImageList.Count - 1)];
    }
}
