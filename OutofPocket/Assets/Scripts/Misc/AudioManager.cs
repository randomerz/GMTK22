using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<AudioSource> narrationAudioSources;
    [SerializeField] private List<AudioSource> sfxAudioSources;
    [SerializeField] private List<AudioSource> musicAudioSources;

    private float narrationVolume;
    private float sfxVolume;
    private float musicVolume;

    [SerializeField] private Slider narrationSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    public float NarrationVolume
    {
        get => narrationVolume;
        set
        {
            narrationVolume = Mathf.Clamp(value, 0, 1);
            narrationAudioSources.ForEach(source => source.volume = narrationVolume);
            PlayerPrefs.SetFloat("narrationVolume", narrationVolume);
        }
    }
    public float SfxVolume
    {
        get => sfxVolume;
        set
        {
            sfxVolume = Mathf.Clamp(value, 0, 1);
            sfxAudioSources.ForEach(source => source.volume = narrationVolume);
            PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        }
    }
    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = Mathf.Clamp(value, 0, 1);
            musicAudioSources.ForEach(source => source.volume = narrationVolume);
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
        }
    }

    private void Awake()
    {
        InitializeSingleton();

        NarrationVolume = PlayerPrefs.GetFloat("narrationVolume");
        SfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        MusicVolume = PlayerPrefs.GetFloat("musicVolume");

        narrationSlider.onValueChanged.AddListener((volume) => NarrationVolume = volume);
        sfxSlider.onValueChanged.AddListener((volume) => SfxVolume = volume);
        musicSlider.onValueChanged.AddListener((volume) => MusicVolume = volume);

        narrationSlider.value = NarrationVolume;
        sfxSlider.value = SfxVolume;
        musicSlider.value = MusicVolume;
    }
}
