using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private float extinguishTime = 3f;
    private Coroutine extinguishRoutine;

    void Awake()
    {
        if (!fireParticles)
            fireParticles = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Foam") && extinguishRoutine == null)
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
        if (fireParticles.isStopped) yield break;  // Evita riattivazioni

        float elapsed = 0f;
        while (elapsed < extinguishTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        fireParticles.Stop();
        gameObject.SetActive(false);
    }
}
