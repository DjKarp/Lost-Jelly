using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        m_Transform = transform;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        _playerDirection = m_InputManager.GetMoveDirection();

        if (!_isMovePlayer && _playerDirection != Vector2.zero)
        {
            _isMovePlayer = true;
            m_Animator.SetBool("IsMove", _isMovePlayer);
            m_SandClock.StartSandClock();
            StartCoroutine(PlayerMove());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Jelly m_Jelly = collision.gameObject.GetComponent<Jelly>();
        if (m_Jelly != null)
        {
            m_Jelly.gameObject.SetActive(false);
            Debug.LogError("AM2222!");
        }
        else
        {
            _isMovePlayer = false;
            StopCoroutine(PlayerMove());
            Debug.LogError("Game Over!");
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
}
