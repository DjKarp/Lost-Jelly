using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounTime : MonoBehaviour
{
    private TMP_Text _countTimeText;
    [SerializeField] private Player m_Player;
    private bool _isTimerGO = false;
    private float _timer = 0.0f;

    private void Awake()
    {
        _countTimeText = gameObject.GetComponent<TMP_Text>();

        StartCoroutine(Timer());

        m_Player.StateGameChanged += ChangeStateTimer;
    }

    public IEnumerator Timer()
    {
        _timer = 0.0f;
        SetTimerValueOnUI();

        while (true)
        {
            if (_isTimerGO)
            {
                _timer += 1.0f;
                SetTimerValueOnUI();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void ChangeStateTimer(bool isTimerGo)
    {
        _isTimerGO = isTimerGo;
    }

    private void SetTimerValueOnUI()
    {
        _countTimeText.text = _timer.ToString();
    }

    private void OnDisable()
    {
        m_Player.StateGameChanged -= ChangeStateTimer;
    }
}
