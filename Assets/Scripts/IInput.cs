using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public event Action<Vector2> ClickDown;
    public event Action<Vector2> ClickUp;
    public event Action<Vector2> ClicRight;
    public event Action<Vector2> ClickLeft;
}
