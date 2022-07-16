using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[System.Serializable]
public class FMODSound
{
    public string name;

    public EventReference fmodEvent;

    [HideInInspector]
    public StudioEventEmitter emitter;
    // public FMOD.Studio.EventInstance sound;
}