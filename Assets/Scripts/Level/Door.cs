using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        m_Animator.SetBool("isOpen", true);
    }
}
