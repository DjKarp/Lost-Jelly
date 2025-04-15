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

    public ReplaySubject<Unit> ReplaySubjectJellyCatch = new ReplaySubject<Unit>();
    public Subject<bool> FinishLevel = new Subject<bool>();

    private void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();

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

            if (collision.TryGetComponent(out FinishLevel finishLevel))
            {
                FinishLevel?.OnNext(true);
            }
            else
            {
                EventManager.GameStartStop(false);
                FinishLevel?.OnNext(false);
                Debug.LogError("Game Over!");
            }

            FinishLevel.OnCompleted();
        }
    }

    public void CatchJelly(Jelly jelly)
    {
        ReplaySubjectJellyCatch?.OnNext(Unit.Default);
        jelly.Deactivate();
    }

    private void StartStopMove(bool isStart)
    {
        m_Animator.SetBool("IsMove", isStart);
    }
}
