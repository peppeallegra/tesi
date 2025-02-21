using UnityEngine;
using System;

public class FireAlarmButton : MonoBehaviour
{
    [SerializeField] private AudioSource alarmSound;
    private bool isActivated = false;
    public static event Action<bool> OnAlarmTriggered;

    void Awake()
    {
        if (alarmSound) alarmSound.Stop();
    }

    public void ToggleAlarm()
    {
        if (!alarmSound) return;

        isActivated = !isActivated;
        if (isActivated) alarmSound.Play();
        else alarmSound.Stop();

        OnAlarmTriggered?.Invoke(isActivated);
    }
}
