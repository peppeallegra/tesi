using UnityEngine;

public class FireAlarmButton : MonoBehaviour
{
    public AudioSource alarmSound;
    private bool isActivated = false;

    private void Start()
    {
        if (alarmSound != null)
        {
            alarmSound.Stop();
        }
    }

    public void ToggleAlarm()
    {
        Debug.Log("🔴 Pulsante premuto! Interazione avvenuta."); // Messaggio di debug

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
    }
}
