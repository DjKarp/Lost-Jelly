using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Volume Mute is Attached to the mute button. And add a slider if it Controls the volume.
/// </summary> 
public class VolumeMute : MonoBehaviour
{

    [SerializeField] private AudioManager.Volume _volume;
    [SerializeField] private GameObject _muteImage;
    [SerializeField] private Slider _slider;
    private Button _button;
    private bool _isMute = false;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.Add(MuteOnOff);
        _isMute = _volume == AudioManager.Volume.FX ? AudioManager.Instance.VolumeFXVCA == 0.0f : AudioManager.Instance.VolumeMusicVCA == 0.0f;
        _slider.onValueChanged.AddListener(_ => SliderChangedValue(_));
        UpdateButton(true);
    }

    public void MuteOnOff()
    {
        _isMute = !_isMute;
        UpdateButton();
    }

    private void SliderChangedValue(float sliderValue)
    {
        if (sliderValue == 0.0f && _isMute == false)
        {
            _isMute = true;
            UpdateButton();
        }
        else if (sliderValue > 0 && _isMute == true)
        {
            _isMute = false;
            UpdateButton();
        }
    }

    public void UpdateButton(bool isSimpleUpdate = false)
    {
        _muteImage.SetActive(_isMute);
        if (!isSimpleUpdate) 
            _slider.value = AudioManager.Instance.VolumeChange(_volume, _isMute);
    }    
}
