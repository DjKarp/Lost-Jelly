using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Jelly : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private Vector2 _blickPosition;
    public Vector2 BlickPosition { get => _blickPosition; }

    public void Initialize(List<Sprite> _jellyImageList)
    {
        // Меняем внешний вид Jelly на рандоминый
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = _jellyImageList[Random.Range(0, _jellyImageList.Count - 1)];

        _blickPosition = new Vector3(m_SpriteRenderer.bounds.min.x + (m_SpriteRenderer.bounds.size.x / 8.0f), m_SpriteRenderer.bounds.max.y - (m_SpriteRenderer.bounds.size.y / 8.0f), 0.0f);
    }
}
