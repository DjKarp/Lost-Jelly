using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using R3;

public class CountTime : MonoBehaviour
{
    private TMP_Text _countTimeText;
    private float _timer = 0.0f;
    private bool _isTimerWork = false;
    private CompositeDisposable _disposable = new CompositeDisposable();
    [SerializeField] private PressAnyKeyToStart m_PressAnyKeyToStart;

    private void Start()
    {
        _countTimeText = gameObject.GetComponent<TMP_Text>();

        EventManager.PlayerMove.AddListener(ChangeStateTimer);

        m_PressAnyKeyToStart.OnGameplayStart
            .Take(1)
            .Subscribe(_ => StartCoroutine(Timer()))
            .AddTo(_disposable);
    }

    private void ChangeStateTimer(bool isStart)
    {
        _isTimerWork = isStart;
    }

    private void SetTimerValueOnUI()
    {
        _countTimeText.text = _timer.ToString();
    }    

    public IEnumerator Timer()
    {
        _timer = 0.0f;
        SetTimerValueOnUI();

        while (true)
        {
            if (_isTimerWork)
            {
                _timer += 1.0f;
                SetTimerValueOnUI();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    public int GetTimerValue()
    {
        return (int)_timer;
    }

    private void OnDisable()
    {
        EventManager.PlayerMove.RemoveListener(ChangeStateTimer);
        _disposable.Dispose();
    }
}
