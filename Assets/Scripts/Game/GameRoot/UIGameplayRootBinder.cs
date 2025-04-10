using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIGameplayRootBinder : MonoBehaviour
{
    public event Action GoToMainMenuButtonClicked;

    public void HandleGoToMainMenuButtonClick()
    {
        GoToMainMenuButtonClicked?.Invoke();
    }
}
