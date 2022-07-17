using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using System;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<Sound> narration;
    [SerializeField] private List<FMODSound> sounds;
    [SerializeField] private List<FMODSound> music;

    private float narrationVolume;
    private float sfxVolume;
    private float musicVolume;

    [SerializeField] private Slider narrationSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private static FMOD.Studio.Bus sfxBus;
    private static FMOD.Studio.Bus musicBus;

    public float NarrationVolume
    {
        get => narrationVolume;
        set
        {
            narrationVolume = Mathf.Clamp(value, 0, 1);
            narration.ForEach(source => source.volume = narrationVolume);
            PlayerPrefs.SetFloat("narrationVolume", narrationVolume);
        }
    }
    public float SfxVolume
    {
        get => sfxVolume;
        set
        {
            sfxVolume = Mathf.Clamp(value, 0, 1);
            musicBus.setVolume(sfxVolume);
            PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        }
    }
    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = Mathf.Clamp(value, 0, 1);
            musicBus.setVolume(musicVolume);
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
        }
    }

    private void Awake()
    {
        InitializeSingleton();

        foreach (FMODSound s in sounds)
        {
            s.emitter = gameObject.AddComponent<StudioEventEmitter>();
            s.emitter.EventReference = s.fmodEvent;
        }

        foreach (FMODSound s in music)
        {
            s.emitter = gameObject.AddComponent<StudioEventEmitter>();
            s.emitter.EventReference = s.fmodEvent;
        }

        foreach (Sound s in narration)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        // Web is async so we do this in start
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX Bus");
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music Bus");


        NarrationVolume = PlayerPrefs.GetFloat("narrationVolume");
        SfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        MusicVolume = PlayerPrefs.GetFloat("musicVolume");

        narrationSlider.onValueChanged.AddListener((volume) => NarrationVolume = volume);
        sfxSlider.onValueChanged.AddListener((volume) => SfxVolume = volume);
        musicSlider.onValueChanged.AddListener((volume) => MusicVolume = volume);

        narrationSlider.value = NarrationVolume;
        sfxSlider.value = SfxVolume;
        musicSlider.value = MusicVolume;

        PlayMusic("High Rollers");
    }

    public static void PlayNarration(string name)
    {
        if (_instance.narration == null)
            return;
        Sound s = _instance.narration.Find(narration => narration.name == name);

        if (s == null)
        {
            Debug.LogError("Narration: " + name + " not found!");
            return;
        }

        if (s.doRandomPitch)
            s.source.pitch = s.pitch * UnityEngine.Random.Range(.95f, 1.05f);
        else
            s.source.pitch = s.pitch;

        s.source.Play();
    }

    public static void PlaySound(string name, float volumeMultiplier=1)
    {
        if (_instance.sounds == null)
            return;
        FMODSound s = _instance.sounds.Find(sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return;
        }
        
        FMODUnity.RuntimeManager.PlayOneShot(s.fmodEvent, volumeMultiplier);
    }

    public static void PlayMusic(string name, bool stopOtherTracks = true)
    {
        FMODSound m = GetMusic(name);

        if (m == null)
        {
            Debug.LogError("Music: " + name + " not found!");
            return;
        }

        if (stopOtherTracks)
        {
            foreach (FMODSound music in _instance.music)
            {
                music.emitter.Stop();
            }
        }

        Debug.Log("Playing music!");
        m.emitter.Play();
    }

    public static void SetMusicParameter(string name, string parameterName, float value)
    {
        // for global parameters
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, value);

        FMODSound m = GetMusic(name);

        if (m == null)
            return;

        // for track-specific parameters
        m.emitter.SetParameter(parameterName, value);
    }

    private static FMODSound GetMusic(string name)
    {
        return _instance.music.Find(music => music.name == name);
    }



    public static void StopAllSoundAndMusic()
    {
        foreach (FMODSound m in _instance.music)
        {
            m.emitter.Stop();
        }
        foreach (FMODSound s in _instance.sounds)
        {
            s.emitter.Stop();
        }
    }
}
