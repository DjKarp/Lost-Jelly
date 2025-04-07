using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform m_Transform;
    private Animator m_Animator;    

    private float _playerSpeed = 30.0f;

    private Vector2 _playerDirection = new Vector2(0, 1);

    private bool _isMovePlayer = false;
    private float _snapValue = 0.425f;

    private void Awake()
    {
        m_Transform = transform;
        m_Animator = gameObject.GetComponent<Animator>();        
    }

    private void Update()
    {
        if (!_isMovePlayer && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            _isMovePlayer = true;
            m_Animator.SetBool("IsMove", _isMovePlayer);
            StartCoroutine(PlayerMove());
        }

        // Test Move
        /*
        if (Input.GetAxis("Horizontal") < 0)
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        else if (Input.GetAxis("Horizontal") > 0)
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        else if (Input.GetAxis("Vertical") < 0)
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        else if (Input.GetAxis("Vertical") > 0)
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        */
        if (Input.GetAxis("Horizontal") < 0)
        {
            _playerDirection = new Vector2(-1.0f, 0.0f);
            m_Transform.localScale = new Vector3(Mathf.Abs(m_Transform.localScale.x), m_Transform.localScale.y, m_Transform.localScale.z);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _playerDirection = new Vector2(1.0f, 0.0f);
            m_Transform.localScale = new Vector3(Mathf.Abs(m_Transform.localScale.x) * -1.0f, m_Transform.localScale.y, m_Transform.localScale.z);
        }
        else if (Input.GetAxis("Vertical") < 0)
            _playerDirection = new Vector2(0, -1);
        else if (Input.GetAxis("Vertical") > 0)
            _playerDirection = new Vector2(0, 1);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Jelly m_Jelly = collision.gameObject.GetComponent<Jelly>();
        if (m_Jelly != null)
        {
            m_Jelly.gameObject.SetActive(false);
            Debug.LogError("AM2222!");
        }
    }

    public IEnumerator PlayerMove()
    {
        while(_isMovePlayer)
        {
            Vector2 newPos = _playerDirection* _snapValue;
            m_Transform.position = m_Transform.position + new Vector3(newPos.x, newPos.y, 0.0f);
            yield return new WaitForSeconds((1.0f / 60.0f) * _playerSpeed);
        }
    }    
}
