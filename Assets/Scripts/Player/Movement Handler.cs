using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private InputManager m_InputManager;

    private Vector2 _playerDirection = new Vector2(0, 0);

    private bool _isMovePlayer = false;
    private float _snapValue = 0.5f;

    public float _playerSpeed => 30;

    public Transform Transform => transform;


    private void Awake()
    {
        EventManager.GameStateChanged.AddListener(StartStopMove);
        EventManager.MoveDirectionChanged.AddListener(ChangeDirectionMove);
    }

    private void Update()
    {
        if (m_InputManager == null)
            m_InputManager = FindObjectOfType<InputManager>();
        else
            _playerDirection = m_InputManager.GetMoveDirection();

        if (!_isMovePlayer && _playerDirection != Vector2.zero)
            StartCoroutine(PlayerMove());
    }

    public IEnumerator PlayerMove()
    {
        _isMovePlayer = true;
        EventManager.GameStateChanged?.Invoke(_isMovePlayer);

        while (_isMovePlayer)
        {
            Transform.localScale = new Vector3(Mathf.Abs(Transform.localScale.x) * (_playerDirection.x > 0 ? -1.0f : 1.0f), Transform.localScale.y, Transform.localScale.z);

            Vector2 offset = _playerDirection * _snapValue;
            Transform.position = Transform.position + new Vector3(offset.x, offset.y, 0.0f);

            yield return new WaitForSeconds((1.0f / 60.0f) * _playerSpeed);
        }
    }

    private void StartStopMove(bool isStop)
    {
        if (!isStop)
            StopCoroutine(PlayerMove());
        /*else
            StartCoroutine(PlayerMove());*/
    }

    private void ChangeDirectionMove(Vector2 newDir)
    {

    }
}
