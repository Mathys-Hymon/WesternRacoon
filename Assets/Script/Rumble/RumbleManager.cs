using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class RumbleManager : MonoBehaviour
{
    [SerializeField] private float lowFrequency;
    [SerializeField] private float highFrequency;
    [SerializeField] private float duration;

    public static RumbleManager instance;

    private Gamepad pad;

    private Coroutine stopRumbleAfterTimeCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            RumblePulse(lowFrequency, highFrequency, duration);
        }
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        pad = Gamepad.current;

        if(pad != null)
        {
            pad.SetMotorSpeeds(lowFrequency, highFrequency);

            stopRumbleAfterTimeCoroutine = StartCoroutine(StopRumble(duration, pad));
        }
    }

    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;
        while(elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pad.SetMotorSpeeds(0f, 0f);
    }
}
