using UnityEngine;

/// <summary>
/// An example of a "helper" when writing code. 
/// </summary> 

public static class ComponentExtention
{
    public static void Activate(this Component component) => component.gameObject.SetActive(true);

    public static void Deactivate(this Component component) => component.gameObject.SetActive(false);
}
