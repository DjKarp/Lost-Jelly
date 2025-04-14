using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static readonly UnityEvent JellyCatched = new UnityEvent();
    public static readonly UnityEvent<int> JellyCount = new UnityEvent<int>();

    public static readonly UnityEvent<bool> PlayerMove = new UnityEvent<bool>();


    static public void CallJellyCatched ()
    {
        JellyCatched?.Invoke();
    }

    static public void SetJellyCount(int count)
    {
        JellyCount?.Invoke(count);
    }

    static public void GameStartStop(bool isStart)
    {
        PlayerMove?.Invoke(isStart);
    }
}
