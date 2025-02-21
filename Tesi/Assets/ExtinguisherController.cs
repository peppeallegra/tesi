using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtinguisherController : MonoBehaviour
{
    public ParticleSystem foamParticles; 
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        if (foamParticles == null)
        {
            Debug.LogError("FoamParticles non è stato assegnato! Assicurati di trascinarlo nell'Inspector.");
        }

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable non trovato sull'estintore! Aggiungilo.");
        }
        else
        {
            grabInteractable.activated.AddListener(StartFoam);
            grabInteractable.deactivated.AddListener(StopFoam);
        }
    }

    void StartFoam(ActivateEventArgs args)
{
    if (foamParticles != null)
    {
        foamParticles.transform.forward = transform.forward; // Allinea alla direzione dell'estintore
        foamParticles.Play();
    }
}


    void StopFoam(DeactivateEventArgs args)
    {
        if (foamParticles != null)
        {
            Debug.Log("Schiuma disattivata!");
            foamParticles.Stop();
        }
    }
}