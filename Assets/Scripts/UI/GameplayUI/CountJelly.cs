
using UnityEngine;
using TMPro;
using R3;
using DG.Tweening;

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

    private void OnDestroy()
    {
        EventManager.JellyCount.RemoveListener(ChangeCountText);
    }
}
