using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementHandler))]
public class Player : MonoBehaviour
{
    private Animator m_Animator;
    

    private void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();

        EventManager.PlayerMove.AddListener(StartStopMove);

        /*
        this.OnCollisionEnter2DAsObservable().Subscribe(x =>
       {
           Debug.LogError("Trigger");
           Debug.LogError(x.gameObject.name);
       });*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Jelly m_Jelly))
        {
            CatchJelly(m_Jelly);
        }
        else
        {
            StartStopMove(false);
        }
    }

    public void CatchJelly(Jelly jelly)
    {
        EventManager.CallJellyCatched();
        jelly.Deactivate();
        Debug.LogError("AM2222!");
    }

    private void StartStopMove(bool isStart)
    {
        m_Animator.SetBool("IsMove", isStart);

        if (!isStart)
        {
            EventManager.PlayerMove?.Invoke(false);
            Debug.LogError("Game Over!");
        }
    }
}
