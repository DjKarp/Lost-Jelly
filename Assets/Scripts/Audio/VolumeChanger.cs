using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script for a slider for Music or Sounds.We hang it on the slider and, if there is, add a component with a button to completely mute the desired bus.
/// VolumeMute - Volume Mute is Attached to the mute button.
/// </summary> 
public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioManager.Volume _volume;
    [SerializeField] private VolumeMute _VolumeMute;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _volume == AudioManager.Volume.FX ? AudioManager.Instance.VolumeFXVCA : AudioManager.Instance.VolumeMusicVCA;
        _slider.onValueChanged.AddListener(_ =>
        {
            SetVolumeSlider(_);
        });       
    }

    public void SetVolumeSlider(float volume)
    {
        switch(_volume)
        {
            case AudioManager.Volume.Musik:
                AudioManager.Instance.SetMusikVolume(volume);
                break;

            case AudioManager.Volume.FX:
                AudioManager.Instance.SetFXVolume(volume);
                break;
        }
    }
}
