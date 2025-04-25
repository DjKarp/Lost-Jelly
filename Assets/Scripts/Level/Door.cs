using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator m_Animator;
    private PolygonCollider2D _PolygonCollider2D;

    private void Start()
    {
        _PolygonCollider2D = GetComponent<PolygonCollider2D>();
        _PolygonCollider2D.isTrigger = true;

        m_Animator = gameObject.GetComponent<Animator>();
        EventManager.AllJellyCatched.AddListener(OpenDoor);
    }

    public void OpenDoor()
    {
        m_Animator.SetTrigger("OpenDoorTrigger");
        AudioManager.Instance.Play_DoorOpen();
    }

    public void OnDestroy()
    {
        EventManager.AllJellyCatched.RemoveListener(OpenDoor);
    }
}
