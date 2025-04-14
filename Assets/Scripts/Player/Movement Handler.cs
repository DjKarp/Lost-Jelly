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

    private CompositeDisposable _disposable = new CompositeDisposable();


    public void Initialize(InputManager inputManager, PressAnyKeyToStart pressAnyKeyToStart, bool isLeftDirectionForSprite)
    {
        inputManager._subjectInputManager
            .Subscribe(newDir => ChangeDirectionMove(newDir))
            .AddTo(_disposable);

        pressAnyKeyToStart.OnGameplayStart
            .Subscribe(_ => StartStopMove(true))
            .AddTo(_disposable);

        ChangeDirectionSprite(isLeftDirectionForSprite);
    }

    public IEnumerator PlayerMove()
    {
        while (_isMovePlayer)
        {
            ChangeDirectionSprite(_playerDirection.x < 0);

            Vector2 offset = _playerDirection * _snapValue;
            Transform.position = Transform.position + new Vector3(offset.x, offset.y, 0.0f);

            yield return new WaitForSeconds((1.0f / 60.0f) * _playerSpeed);
        }
    }

    private void StartStopMove(bool isStart)
    {
        _isMovePlayer = isStart;

        if (_isMovePlayer)
            StartCoroutine(PlayerMove());
        else
            StopCoroutine(PlayerMove());
        
        EventManager.PlayerMove?.Invoke(_isMovePlayer);
    }
    public void ChangeDirectionSprite(bool isLeft)
    {
        Transform.localScale = new Vector3(Mathf.Abs(Transform.localScale.x) * (!isLeft ? -1.0f : 1.0f), Transform.localScale.y, Transform.localScale.z);
    }

    private void ChangeDirectionMove(Vector2 newDir)
    {
        _playerDirection = newDir;
    }

    private void OnDisable()
    {
        _disposable.Dispose();
    }
}
