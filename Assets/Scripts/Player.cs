using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

public class Player : MonoBehaviour
{
    private Transform m_Transform;
    private Animator m_Animator;
    [SerializeField] private InputManager m_InputManager;
    [SerializeField] private SandClock m_SandClock;

    private float _playerSpeed = 30.0f;

    private Vector2 _playerDirection = new Vector2(0, 0);

    private bool _isMovePlayer = false;
    private float _snapValue = 0.5f;

    public event Action<bool> StateGameChanged;

    

    private void Awake()
    {
        m_Transform = transform;
        m_Animator = gameObject.GetComponent<Animator>();

        this.OnCollisionEnter2DAsObservable().Subscribe(x =>
       {
           Debug.LogError("Trigger");
           Debug.LogError(x.gameObject.name);
       });
    }

    private void Update()
    {
        if (m_InputManager == null)
            m_InputManager = FindObjectOfType<InputManager>();
        else
            _playerDirection = m_InputManager.GetMoveDirection();

        if (!_isMovePlayer && _playerDirection != Vector2.zero)
        {
            _isMovePlayer = true;
            m_Animator.SetBool("IsMove", _isMovePlayer);
            StateGameChanged?.Invoke(true);
            if (m_SandClock == null)
                m_SandClock = FindObjectOfType<SandClock>();
            m_SandClock.StartSandClock();

            StartCoroutine(PlayerMove());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Jelly m_Jelly))
        {
            CatchJelly(m_Jelly);
        }
        else
        {
            StopGame();
        }
    }

    public IEnumerator PlayerMove()
    {
        while(_isMovePlayer)
        {
            m_Transform.localScale = new Vector3(Mathf.Abs(m_Transform.localScale.x) * (_playerDirection.x > 0 ? - 1.0f : 1.0f), m_Transform.localScale.y, m_Transform.localScale.z);

            Vector2 offset = _playerDirection * _snapValue;
            m_Transform.position = m_Transform.position + new Vector3(offset.x, offset.y, 0.0f);

            yield return new WaitForSeconds((1.0f / 60.0f) * _playerSpeed);
        }
    }

    public void CatchJelly(Jelly jelly)
    {
        EventManager.CallJellyCatched();
        jelly.gameObject.SetActive(false);
        Debug.LogError("AM2222!");
    }

    private void StopGame()
    {
        StateGameChanged?.Invoke(false);
        _isMovePlayer = false;
        StopCoroutine(PlayerMove());
        Debug.LogError("Game Over!");
    }
}
