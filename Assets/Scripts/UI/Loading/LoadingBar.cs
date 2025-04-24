using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private Image _image;

    public void Initialization()
    {
        _image = gameObject.GetComponent<Image>();
        SetImageProgress(0.0f);
    }

    public void SetImageProgress(float loadingValue)
    {
        _image.fillAmount = loadingValue;
    }
}
