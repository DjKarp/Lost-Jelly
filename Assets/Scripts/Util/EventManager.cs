using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static readonly UnityEvent JellyCatched = new UnityEvent();
    public static readonly UnityEvent<int> JellyCount = new UnityEvent<int>();

    public static readonly UnityEvent<bool> GameStateChanged = new UnityEvent<bool>();

    public static readonly UnityEvent<Vector2> MoveDirectionChanged = new UnityEvent<Vector2>();


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
        GameStateChanged?.Invoke(isStart);
    }

    static public void MoveDirChanged(Vector2 vector2)
    {
        MoveDirectionChanged?.Invoke(vector2);
    }
}
