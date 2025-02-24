using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private float extinguishTime = 3f;
    private Coroutine extinguishRoutine;
    
    // Aggiungiamo questa variabile per sapere se il fuoco è stato estinto
    private bool isExtinguished = false;

    // Proprietà pubblica (solo lettura) per permettere ad altri script di controllare se il fuoco è spento
    public bool IsExtinguished => isExtinguished;

    void Awake()
    {
        if (!fireParticles)
            fireParticles = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Foam") && extinguishRoutine == null && !isExtinguished)
        {
            extinguishRoutine = StartCoroutine(ExtinguishFire());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Foam") && extinguishRoutine != null)
        {
            StopCoroutine(extinguishRoutine);
            extinguishRoutine = null;
        }
    }

    private IEnumerator ExtinguishFire()
    {
        if (fireParticles.isStopped) yield break;  // Già estinto

        float elapsed = 0f;
        while (elapsed < extinguishTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Fuoco estinto
        fireParticles.Stop();
        gameObject.SetActive(false);
        isExtinguished = true;
    }
}
