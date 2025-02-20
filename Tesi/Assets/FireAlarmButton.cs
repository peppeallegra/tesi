using UnityEngine;
using System;

public class FireAlarmButton : MonoBehaviour
{
    public AudioSource alarmSound;
    private bool isActivated = false;

    public static event Action<bool> OnAlarmTriggered; // Evento per notificare l'attivazione dell'allarme

    private void Start()
    {
        if (alarmSound != null)
        {
            alarmSound.Stop();
        }
    }

    public void ToggleAlarm()
    {
        Debug.Log("🔴 Pulsante premuto! Interazione avvenuta."); 

        if (alarmSound == null) return;

        isActivated = !isActivated;

        if (isActivated)
        {
            Debug.Log("🚨 Allarme ATTIVATO!");
            alarmSound.Play();
        }
        else
        {
            Debug.Log("🔕 Allarme DISATTIVATO!");
            alarmSound.Stop();
        }

        // Notifica tutti gli NPC dell'attivazione/disattivazione dell'allarme
        OnAlarmTriggered?.Invoke(isActivated);
    }
}
