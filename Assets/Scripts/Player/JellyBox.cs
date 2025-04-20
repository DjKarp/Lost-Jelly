using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class JellyBox : MonoBehaviour
{
    private Player m_Player;   
    private CompositeDisposable _disposables = new CompositeDisposable();
    private Transform m_Transform;
    private int _number;

    public void Initialize(Player player, Subject<Vector2> subject, int number)
    {
        m_Transform = transform;
        m_Player = player;
        _number = number;

        subject
            .Subscribe(_ => NewPositionMove(_))
            .AddTo(_disposables);
    }

    private void NewPositionMove(Vector2 vector2)
    {
        m_Transform.position = m_Player.GetNewPositionJellyBox(_number);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
