using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class MovementHandler : MonoBehaviour
{
    private Vector2 _playerDirection = new Vector2(0, 0);

    private bool _isMovePlayer = false;
    private float _snapValue = 0.5f;

    public float _playerSpeed => 30;

    public Transform Transform => transform;
    public float PlayerSpeedOnSecond { get => (1.0f / 60.0f) * _playerSpeed; }

    private CompositeDisposable _disposable = new CompositeDisposable();


    public void Initialize(InputManager inputManager, PressAnyKeyToStart pressAnyKeyToStart, bool isLeftDirectionForSprite)
    {
        inputManager.SubjectInputManager
            .Subscribe(newDir => ChangeDirectionMove(newDir))
            .AddTo(_disposable);

        pressAnyKeyToStart.OnGameplayStart
            .Subscribe(_ => StartMove())
            .AddTo(_disposable);

        ChangeDirectionSprite(isLeftDirectionForSprite);

        EventManager.PlayerMove.AddListener(StopMove);

        Observable
            .Interval(TimeSpan.FromSeconds(PlayerSpeedOnSecond))
            .Subscribe(_ => MovePlayer())
            .AddTo(_disposable);
    }
    private void MovePlayer()
    {
        if (_isMovePlayer)
        {
            if (_playerDirection.x != 0.0f) 
                ChangeDirectionSprite(_playerDirection.x < 0.0f);

            Vector2 offset = _playerDirection * _snapValue;
            Transform.position = Transform.position + new Vector3(offset.x, offset.y, 0.0f);
        }
    }

    private void StartMove()
    {
        _isMovePlayer = true;
        
        EventManager.GameStartStop(_isMovePlayer);
    }

    private void StopMove(bool isMove)
    {
        _isMovePlayer = isMove;
    }
    public void ChangeDirectionSprite(bool isLeft)
    {
        Transform.localScale = new Vector3(Mathf.Abs(Transform.localScale.x) * (!isLeft ? -1.0f : 1.0f), Transform.localScale.y, Transform.localScale.z);
    }

    private void ChangeDirectionMove(Vector2 newDir)
    {
        _playerDirection = newDir;
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
