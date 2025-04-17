using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator m_Animator;
    private PolygonCollider2D m_PolygonCollider2D;

    private void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_PolygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        EventManager.AllJellyCatched.AddListener(OpenDoor);
    }

    public void OpenDoor()
    {
        m_Animator.SetBool("isOpen", true);
        //m_PolygonCollider2D.Deactivate();
    }

    public void OnDestroy()
    {
        EventManager.AllJellyCatched.RemoveListener(OpenDoor);
    }
}
