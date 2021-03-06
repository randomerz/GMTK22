using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is just here to show how you can use NarrationManager in case it helps anyone.
/// Attach this to a GameObject and have fun.
/// </summary>
public class NarrationManagerDemo : MonoBehaviour
{
    void Start()
    {
        NarrationManager.OnVoiceClipStarted += OnStartEvent;
        NarrationManager.OnVoiceClipFinished += OnEndEvent;

        Invoke("Test", 1.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Test();
        }
    }

    void Test()
    {
        NarrationManager.PlayVoiceClip("Optimist/HelloWelcomeTo", Three, new CallbackWithDelay(1.0f, One), new CallbackWithDelay(2.0f, Two));
    }

    void OnStartEvent() // Happens when any clip starts
    {
        Debug.Log("Start Event");
    }
    void OnEndEvent() // Happens when any clip ends
    {
        Debug.Log("End Event");
    }
    void One() // Happens 1s into the clip we played
    {
        Debug.Log("One");
    }
    void Two() // Happens 2s into the clip we played
    {
        Debug.Log("Two");
    }
    void Three() // Happens when the clip we played ends
    {
        Debug.Log("Three");
    }
}
