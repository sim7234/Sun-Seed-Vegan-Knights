using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private Gamepad pad;

    private Coroutine stopRumbleAfterTimeCoroutine;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration, Gamepad aPad)
    {
        pad = aPad;

        if (pad != null)
        {
            pad.SetMotorSpeeds(lowFrequency, highFrequency);
        }

        stopRumbleAfterTimeCoroutine = StartCoroutine(StopRumble(duration, pad));
    }

    private IEnumerator StopRumble(float duration, Gamepad aPad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        aPad.SetMotorSpeeds(0, 0);
    }
    
}
