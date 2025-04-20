using UnityEngine;
using R3;

public class Leaves : Effect
{
    private SpriteRenderer _SpriteRenderer;
    private Vector2 _startPosition;
    private Transform _transform;
    private float _speed = 5.0f;
    private float _startSpeed = 3.0f;

    public override void Initialize(Vector2 startPosition, Sprite sprite)
    {
        _transform = gameObject.transform;
        _SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _SpriteRenderer.sprite = sprite;

        _transform.position = _startPosition = startPosition;
        _transform.rotation = Quaternion.Euler(_transform.rotation.x, transform.rotation.y, transform.rotation.z + Random.Range(-180, 180));

        _speed = Random.Range(_startSpeed - (_startSpeed / 1.2f), _startSpeed + _startSpeed);

        _timer = 0.01f;
        Observable
            .EveryUpdate()
            .Subscribe(_ => Action())
            .AddTo(_disposables);
    }

    protected override void Action()
    {
        if (Time.timeScale > 0)
            _transform.position = new Vector3(_transform.position.x - (_speed * Time.deltaTime), _transform.position.y + Mathf.Sin(Time.time * Mathf.Clamp(Random.Range(1.0f, 30.0f) * 0.01f, 1.0f, 3.0f)) * 0.01f, 0.0f);
        CheckOnDisable();
    }

    public void CheckOnDisable()
    {
        if (Vector2.Distance(_startPosition, transform.position) > 50.0f)
            gameObject.SetActive(false);
    }
}
