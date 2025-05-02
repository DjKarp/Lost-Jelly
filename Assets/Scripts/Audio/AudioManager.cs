using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;
using R3;

/// <summary>
/// The singleton responsible for all musical accompaniment. It sets music and sounds automatically according to the list of levels and sounds associated with them. 
/// It is the singleton that is needed, since FMOD knows how not to interrupt the music between scene downloads, and the singleton is just monitoring whether it is necessary to switch the composition.
/// </summary> 

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    private CompositeDisposable _disposables = new CompositeDisposable();

    private Camera _camera;
    private GameObject Camera  
    { 
        get => _camera.gameObject;
    }

    public enum Volume
    {
        Musik,
        FX
    }

    private VCA _MusicVCA;
    private VCA _FXVCA;
    private float _volumeMusicVCA;
    private float _volumeFXVCA;

    public float VolumeMusicVCA { get => _volumeMusicVCA; }
    public float VolumeFXVCA { get => _volumeFXVCA; }

    /// <summary>
    /// Below is a list of FMOD events. As well as individual events.
    /// </summary>
    //Melody
    private string _Gameplay_Musik = "event:/Musik/Gameplay_Musik_ok";

    //Gameplay
    private string _CollisionBad = "event:/Gameplay/CollisionBad";
    private string _Nimble_Am = "event:/Gameplay/Nimble_Am";
    private string _DoorOpen = "event:/Gameplay/DoorOpen";
    private string _GoodGame = "event:/ShortMusik/GoodGame";
    private string _LooseLevel = "event:/ShortMusik/LooseLevel";

    //FX
    private string _ClickStartGame = "event:/ShortMusik/ClickStartGame";
    private string _ButtonClick = "event:/UIFX/ButtonClick";

    //FMOD Events
    private EventInstance _musicEvent;
    private EventDescription _musicDes;
    private PARAMETER_DESCRIPTION _musicPD;
    private PARAMETER_ID _musicPiD;
    private string _tempPath;

    private SaveLoadData _saveLoadData;


    public void Initialization(SaveLoadData saveLoadData)
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _saveLoadData = saveLoadData;

        FindCameraListener();

        _MusicVCA = RuntimeManager.GetVCA("vca:/Musik");
        _FXVCA = RuntimeManager.GetVCA("vca:/FX");

        SetFXVolume(saveLoadData.GetVolumeFX());
        SetMusikVolume(saveLoadData.GetVolumeMusik());
    }

    public void PlayAudio(bool isMainMenu)
    {
        FindCameraListener();
        PlayingMusic(isMainMenu);
    }
    
    private void FindCameraListener()
    {
        _camera = FindObjectOfType<Camera>();
    }

    public void StopMusic()
    {
        _musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }    

    public float VolumeChange(Volume volume, bool isMute)
    {
        float value = isMute ? 0.0f : 0.8f;
        switch (volume)
        {
            case Volume.Musik:
                SetMusikVolume(value);
                return value;

            case Volume.FX:
                SetFXVolume(value);
                return value;
        }
        return value;
    }

    public void SetMusikVolume(float volume)
    {
        _MusicVCA.setVolume(volume);
        _volumeMusicVCA = volume;
    }
    public void SetFXVolume(float volume)
    {
        _FXVCA.setVolume(volume);
        _volumeFXVCA = volume;
    }

    private void PlayingMusic(bool isMainMenu)
    {
        if (GetMusicPath() != null) StopMusic();

        _musicEvent = RuntimeManager.CreateInstance(_Gameplay_Musik);
        _musicEvent.set3DAttributes(RuntimeUtils.To3DAttributes(Camera));
        _musicEvent.start();

        _musicDes = RuntimeManager.GetEventDescription(_Gameplay_Musik);
        _musicDes.getParameterDescriptionByName("GameplayMusik", out _musicPD);
        _musicPiD = _musicPD.id;

        SetMusiñParameter(isMainMenu ? 0.0f : 0.65f);
    }

    public string GetMusicPath()
    {
        _musicEvent.getDescription(out _musicDes);
        _musicDes.getPath(out _tempPath);
        return _tempPath;
    }

    /// <summary>
    /// Public methods for calling them via a singleton. 
    /// This is where we need to create new methods for new sounds in the game.
    /// Divided by areas of responsibility for easier navigation.
    /// </summary>

    ///////////// = Gameplay = ///////////////    
    public void Play_GoodGame() { RuntimeManager.PlayOneShotAttached(_GoodGame, Camera); }
    public void Play_LooseLevel() { RuntimeManager.PlayOneShotAttached(_LooseLevel, Camera); }
    public void Play_FinishLevel(bool isGG) { if (isGG) Play_GoodGame(); else { Play_CollisionBad(); Play_LooseLevel(); } }
    public void Play_DoorOpen() { RuntimeManager.PlayOneShotAttached(_DoorOpen, Camera); }


    //////// = Player = //////////
    public void Play_CollisionBad() { RuntimeManager.PlayOneShotAttached(_CollisionBad, Camera); }
    public void Play_Nimble_Am() { RuntimeManager.PlayOneShotAttached(_Nimble_Am, Camera); }


    //////// = UI = //////////
    public void Play_ClickStartGame() { RuntimeManager.PlayOneShotAttached(_ClickStartGame, Camera); }
    public void Play_ButtonClick() { RuntimeManager.PlayOneShotAttached(_ButtonClick, Camera); }

    /// <summary>
    /// We set the music event parameter, which, depending on the number of Jelly collected, switches the track to a more aggressive and faster one.
    /// </summary>
    /// <param name="param">From 0 to 1</param>
    public void SetMusiñParameter(float param)
    {
        param = Mathf.Clamp(param, 0.0f, 1.0f);
        _musicEvent.setParameterByID(_musicPiD, param);
    }

    public void SetPlayerSound(ReplaySubject<Unit> catchJellySubject, Subject<bool> finishLevel)
    {
        catchJellySubject
            .Subscribe(_ => CalculateJellysKoefficientOnLevel())
            .AddTo(_disposables);

        finishLevel
            .Subscribe(_ => Play_FinishLevel(_))
            .AddTo(_disposables);
    }

    private int _maxJelly;
    private int _currentJellyCount;

    public void SetJellyCount(int jellyCount)
    {
        _maxJelly = _currentJellyCount = jellyCount;
    }

    private void CalculateJellysKoefficientOnLevel()
    {
        Play_Nimble_Am();
        _currentJellyCount--;
        if (_currentJellyCount < 0.3f * _maxJelly)
            SetMusiñParameter(1.0f);
    }

    private void OnDestroy()
    {
        _saveLoadData.SetVolumeData(_volumeMusicVCA, _volumeFXVCA);
        _disposables.Dispose();
    }
}
