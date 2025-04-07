using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] private List<Sprite> _jellyImageList;

    private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        // Меняем внешний вид Jelly на рандоминый
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _jellyImageList.AddRange(Resources.LoadAll<Sprite>("Jelly"));
        m_SpriteRenderer.sprite = _jellyImageList[Random.Range(0, _jellyImageList.Count - 1)];
    }
}
