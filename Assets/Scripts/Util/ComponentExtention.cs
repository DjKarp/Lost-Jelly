using UnityEngine;

public static class ComponentExtention
{
    public static void Activate(this Component component) => component.gameObject.SetActive(true);

    public static void Deactivate(this Component component) => component.gameObject.SetActive(false);
}
