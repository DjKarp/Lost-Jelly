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
    private CompositeDisposable _disposables = new CompositeDisposable();
    public Subject<Vector2> _moveSubject;

    private JellyBox _jellyBoxPrefab;
    private JellyBox _tempJellyBox;
    private List<JellyBox> _jellyBoxes = new List<JellyBox>();
    private List<Vector2> _positionsList = new List<Vector2>();

    private int _jellyBoxCount = 0;

    public void Initialize(bool isLeftDirection, Subject<Vector2> subject)
    {
        m_Animator = gameObject.GetComponent<Animator>();

        _moveSubject = subject;

        _positionsList.Add(transform.position);

        _jellyBoxPrefab = Resources.Load<JellyBox>("BoxWhitJelly");
        _tempJellyBox = Instantiate(_jellyBoxPrefab, transform.position + new Vector3((MovementHandler._snapValue * (isLeftDirection ? 1 : -1)), 0.0f, 0.0f), Quaternion.identity);
        _tempJellyBox.Initialize(this, _moveSubject, _jellyBoxCount);
        _jellyBoxes.Add(_tempJellyBox);

        subject
            .Subscribe(_ => _positionsList.Insert(0, _))
            .AddTo(_disposables);

        this.OnTriggerEnter2DAsObservable()
            .Subscribe(x =>
            {
                if (x.TryGetComponent(out Jelly m_Jelly))
                {
                    CatchJelly(m_Jelly);
                }
                else
                {
                    StartStopMove(false);

                    if (x.TryGetComponent(out FinishLevel finishLevel))
                    {
                        FinishLevel?.OnNext(true);
                    }
                    else
                    {
                        EventManager.GameStartStop(false);
                        FinishLevel?.OnNext(false);
                        Debug.LogError("Game Over!");
                    }
                }
            });
    }

    public void CatchJelly(Jelly jelly)
    {        
        CreateAndInitializeJellyBox();
        ReplaySubjectJellyCatch?.OnNext(Unit.Default);
        jelly.Deactivate();
    }

    private void StartStopMove(bool isStart)
    {
        m_Animator.SetBool("IsMove", isStart);
    }

    private void CreateAndInitializeJellyBox()
    {
        _jellyBoxCount++;
        _tempJellyBox = Instantiate(_jellyBoxPrefab, GetNewPositionJellyBox(_jellyBoxCount), Quaternion.identity);
        _tempJellyBox.Initialize(this, _moveSubject, _jellyBoxCount);
        _jellyBoxes.Add(_tempJellyBox);
    }

    public Vector2 GetNewPositionJellyBox(int number)
    {
        return _positionsList[number];
    }

    private void OnDestroy()
    {
        FinishLevel.OnCompleted();
        _disposables.Dispose();
    }
}
