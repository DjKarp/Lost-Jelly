using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private IEnumerator Start()
    {
        // Load Screen
        // Zenject
        // UI
        // Input
        // Localozation
        // Storage
        // Scene Management
        yield return new WaitForSeconds(1.0f);

        Debug.LogError("EntryPoint Start");

        yield return new WaitForEndOfFrame();
    }
}
