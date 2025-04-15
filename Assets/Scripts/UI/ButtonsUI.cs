using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonsUI : MonoBehaviour
{
    private Button m_Button;

    protected void Awake()
    {
        m_Button = gameObject.GetComponent<Button>();
        m_Button.Add(ButtonClick);
    }

    protected abstract void ButtonClick();

    private void OnDestroy()
    {
        m_Button.RemoveAll();
    }
}
