using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandClock : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }


    public void StartSandClock()    
    {
        m_Animator.SetBool("isStartGame", true);
    }

    public void StopSandClock()
    {
        m_Animator.SetBool("isStartGame", false);
    }
}
