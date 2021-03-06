using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Singleton component used for playing voice clips. Use <see cref="PlayVoiceClip(string, System.Action, CallbackWithDelay[])"/> to play a voice clip.
/// </summary>
public class NarrationManager : Singleton<NarrationManager>
{
    private AudioSource audioSource;

    public static event Action OnVoiceClipStarted;
    public static event Action OnVoiceClipFinished;

    private void Awake()
    {
        InitializeSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the VoiceClip found at the passed in path and triggers callbacks as appropriate.
    /// If there is already a voice clip playing, this interrupts it immediately without triggering 
    /// any of its callbacks.
    /// </summary>
    /// <param name="path">The path to the VoiceClip, ex: "Optimist/WelcomeToMyMinecraftLetsPlay"</param>
    /// <param name="onComplete">A callback to execute when the clip finishes playing</param>
    /// <param name="afterDelay">Any number of callbacks to execute while the clip plays. If the delay is longer
    /// than the length of the clip, it will not be triggered. Sucks to suck.</param>
    public static void PlayVoiceClip(string path, Action onComplete = null, params CallbackWithDelay[] afterDelay)
    {
        _instance.StopAllCoroutines();
        if (_instance.audioSource == null)
        {
            _instance.audioSource = _instance.GetComponent<AudioSource>();
        }
        VoiceClip voiceClip = Resources.Load<VoiceClip>($"Narration/VoiceClips/{path}");
        if (voiceClip == null)
        {
            Debug.LogError($"The voice clip with path Resources/Narration/VoiceClips/{path} was not found!");
            return;
        }
        _instance.StartCoroutine(_instance.IPlayAudio(voiceClip, onComplete, new List<CallbackWithDelay>(afterDelay)));
    }

    private IEnumerator IPlayAudio(VoiceClip voiceClip, Action onComplete = null, List<CallbackWithDelay> afterDelay = null)
    {
        OnVoiceClipStarted?.Invoke();
        audioSource.clip = voiceClip.AudioClip;
        audioSource.Play();

        UIManager.SetSubtitle(voiceClip.Subtitle, voiceClip.Color, voiceClip.AudioClip.length * 1.25f);

        float timeElapsed = 0;
        while (audioSource.isPlaying)
        {
            timeElapsed += Time.deltaTime;
            for (int i = 0; i < afterDelay.Count; i++)
            {
                if (timeElapsed > afterDelay[i].delayInSeconds)
                {
                    afterDelay[i].callback?.Invoke();
                    afterDelay.Remove(afterDelay[i]);
                }
            }
            yield return null;
        }

        onComplete?.Invoke();
        OnVoiceClipFinished?.Invoke();
    }

    public static void SetVolume(float volume)
    {
        if (_instance != null && _instance.audioSource != null)
        {
            _instance.audioSource.volume = volume;
        }
    }
}

/// <summary>
/// Use this with <see cref="NarrationManager.PlayVoiceClip(string, Action, CallbackWithDelay[])"/> to trigger a callback after a delay.
/// </summary>
public struct CallbackWithDelay
{
    public float delayInSeconds;
    public Action callback;

    public CallbackWithDelay(float delayInSeconds, Action callback)
    {
        this.callback = callback;
        this.delayInSeconds = delayInSeconds;
    }
}
