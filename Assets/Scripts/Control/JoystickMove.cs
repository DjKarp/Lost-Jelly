using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : Joystick
{
    public Vector2 GetMoveDirection()
    {
        return _joystickDirection;
    }
}
