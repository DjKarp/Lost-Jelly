using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using R3;

public class CounTime : MonoBehaviour
{
    private TMP_Text _countTimeText;
    private float _timer = 0.0f;
    private bool isTimerWork = false;
    private CompositeDisposable _disposable = new CompositeDisposable();
    [SerializeField] private PressAnyKeyToStart m_PressAnyKeyToStart;

    private void Awake()
    {
        _countTimeText = gameObject.GetComponent<TMP_Text>();

        EventManager.PlayerMove.AddListener(ChangeStateTimer);

        m_PressAnyKeyToStart.OnGameplayStart
            .Subscribe(_ => StartTimer())
            .AddTo(_disposable);
    }

    private void ChangeStateTimer(bool isStart)
    {
        isTimerWork = isStart;
    }

    private void SetTimerValueOnUI()
    {
        _countTimeText.text = _timer.ToString();
    }

    private void OnDisable()
    {
        EventManager.PlayerMove.RemoveListener(ChangeStateTimer);
        _disposable.Dispose();
    }

    private void StartTimer()
    {
        isTimerWork = true;

        Observable
            .Interval(System.TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => AddTickOnTimer())
            .AddTo(_disposable);
    }
    private void AddTickOnTimer()
    {
        if (isTimerWork)
        {
            _timer++;
            SetTimerValueOnUI();
        }
    }
}
