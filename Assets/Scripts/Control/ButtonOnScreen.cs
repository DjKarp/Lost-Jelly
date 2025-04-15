using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R3;

public class ButtonOnScreen : MonoBehaviour
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;

    private Vector2 _direction;

    private CompositeDisposable _disposable = new CompositeDisposable();
    public Subject<Unit> _subjectButtonOnScreen = new();

    private void Awake()
    {
        _leftButton.Add(() => ButtonPressed(new Vector2(-1, 0)));
        _rightButton.Add(() => ButtonPressed(new Vector2(1, 0)));
        _upButton.Add(() => ButtonPressed(new Vector2(0, 1)));
        _downButton.Add(() => ButtonPressed(new Vector2(0, -1)));
    }

    private void ButtonPressed(Vector2 vector2)
    {
        _direction = vector2;
        _subjectButtonOnScreen.OnNext(Unit.Default);
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }

    private void OnDestroy()
    {
        _leftButton.RemoveAll();
        _rightButton.RemoveAll();
        _upButton.RemoveAll();
        _downButton.RemoveAll();
        _disposable.Dispose();
    }
}
