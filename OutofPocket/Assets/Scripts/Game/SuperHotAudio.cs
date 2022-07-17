using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHotAudio : Singleton<SuperHotAudio>
{
    public static bool isSuperHotOn;
    public Rigidbody cueBody;
    private float currentPitch = 1;
    private float targetPitch = 1;
    private float pitchChangeRate = 2; // 1 per second

    private void Awake()
    {
        InitializeSingleton();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (isSuperHotOn)
        {
            // 0 = 0.5, 5 = 1
            targetPitch = (cueBody.velocity.magnitude / 3f) + 0.5f;
            targetPitch = Mathf.Clamp(targetPitch, 0.5f, 1);
        }
        else
        {
            targetPitch = 1;
        }

        if (currentPitch != targetPitch)
        {
            UpdatePitch();
        }
    }

    private void UpdatePitch()
    {
        if (targetPitch < currentPitch)
            currentPitch = Mathf.Clamp(currentPitch - pitchChangeRate * Time.deltaTime, targetPitch, 1);
        else if (targetPitch > currentPitch)
            currentPitch = Mathf.Clamp(currentPitch + pitchChangeRate * Time.deltaTime, 0.5f, targetPitch);
        else
            return;

        Time.timeScale = currentPitch;
        AudioManager.SetPitch(currentPitch);
    }
}
