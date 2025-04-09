using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CountJelly : MonoBehaviour
{
    private TMP_Text _countJellyText;


    private void Awake()
    {
        _countJellyText = gameObject.GetComponent<TMP_Text>();
        EventManager.JellyCount.AddListener(ChangeCountText);
    }

    private void ChangeCountText(int value)
    {
        _countJellyText.text = value.ToString();
    }
}
