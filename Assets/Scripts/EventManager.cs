using UnityEngine.Events;

public class EventManager
{
    public static readonly UnityEvent JellyCatched = new UnityEvent();
    public static readonly UnityEvent<int> JellyCount = new UnityEvent<int>();

    static public void CallJellyCatched ()
    {
        if (JellyCatched != null)
            JellyCatched.Invoke();
    }

    static public void SetJellyCount(int count)
    {
        if (JellyCount != null)
            JellyCount.Invoke(count);
    }
}
