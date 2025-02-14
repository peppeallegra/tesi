using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public float extinguishTime = 3f; // Tempo necessario per spegnere il fuoco
    private float extinguishProgress = 0f;

    void Start()
    {
        if (fireParticles == null)
            fireParticles = GetComponent<ParticleSystem>();
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("🔥 Il fuoco è colpito da: " + other.gameObject.name + " | Tag: " + other.tag);

        if (other.CompareTag("Foam")) 
        {
            Debug.Log("✅ Schiuma rilevata! Progresso spegnimento: " + extinguishProgress + "/" + extinguishTime);
            extinguishProgress += Time.deltaTime; // Aumenta il timer ogni secondo

            if (extinguishProgress >= extinguishTime)
            {
                Debug.Log("🔥 Fuoco spento!");
                fireParticles.Stop(); // Ferma le fiamme
                gameObject.SetActive(false); // Disattiva l'oggetto
            }
        }
    }
}
