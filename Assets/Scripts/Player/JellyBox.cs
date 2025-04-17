using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class JellyBox : MonoBehaviour
{
    private Player m_Player;
    //private JellyBox _prevJellyBox;    
    private CompositeDisposable _disposables = new CompositeDisposable();
    private Transform m_Transform;
    private int _number;

    public void Initialize(Player player, Subject<Vector2> subject, int number)
    {
        m_Transform = transform;
        m_Player = player;
        _number = number;

        //m_Transform.position = m_Player.ge;

        subject
            .Subscribe(_ => NewPositionMove(_))
            .AddTo(_disposables);
    }

    /*public void AttachNewJellyBox(JellyBox jellyBox, Vector2 position)
    {
        _prevJellyBox = jellyBox;
        transform.position = position;
    }*/

    private void NewPositionMove(Vector2 vector2)
    {
        m_Transform.position = m_Player.GetNewPositionJellyBox(_number);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
