using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandClock : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        EventManager.GameStateChanged.AddListener(StartStopSandClock);
    }

    public void StartStopSandClock(bool isStart)    
    {
        m_Animator.SetBool("isStartGame", isStart);
    }

    private void OnDisable()
    {
        EventManager.GameStateChanged.RemoveListener(StartStopSandClock);
    }
}
