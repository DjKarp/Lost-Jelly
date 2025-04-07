using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator m_Animator;

    private float speed = 2.0f;

    private void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        m_Animator.SetBool("IsMove", Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);

        if (Input.GetAxis("Horizontal") < 0)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        else if (Input.GetAxis("Horizontal") > 0)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else if (Input.GetAxis("Vertical") < 0)
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        else if (Input.GetAxis("Vertical") > 0)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jelly m_Jelly = collision.gameObject.GetComponent<Jelly>();
        if (m_Jelly != null)
        {
            m_Jelly.gameObject.SetActive(false);
            Debug.LogError("AM!");
        }
    }
}
