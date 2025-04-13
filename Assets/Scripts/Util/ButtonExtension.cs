using UnityEngine.Events;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void Add(this Button button, UnityAction unityAction)
        => button.onClick.AddListener(unityAction);

    public static void Remove(this Button button, UnityAction unityAction)
        => button.onClick.RemoveListener(unityAction);

    public static void RemoveAll(this Button button)
        => button.onClick.RemoveAllListeners();
}
