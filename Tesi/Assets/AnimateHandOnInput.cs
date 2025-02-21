using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    private float lastTriggerValue = -1f;
    private float lastGripValue = -1f;

    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        if (Mathf.Abs(triggerValue - lastTriggerValue) > 0.01f) 
        {
            handAnimator.SetFloat("Trigger", triggerValue);
            lastTriggerValue = triggerValue;
        }

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        if (Mathf.Abs(gripValue - lastGripValue) > 0.01f)
        {
            handAnimator.SetFloat("Grip", gripValue);
            lastGripValue = gripValue;
        }
    }
}
