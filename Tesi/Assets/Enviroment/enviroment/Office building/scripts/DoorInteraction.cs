using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorInteraction : MonoBehaviour
{
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public float rotationAngle = 90f;  // Angolo di apertura
    public float speed = 2f;  // Velocità di apertura

    void Start()
    {
        closedRotation = transform.rotation; // Salva la rotazione iniziale
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + rotationAngle, transform.eulerAngles.z);
    }

    public void ToggleDoor()
    {
        if (!isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(RotateDoor(openRotation));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(RotateDoor(closedRotation));
        }
        isOpen = !isOpen;
    }

    private System.Collections.IEnumerator RotateDoor(Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
