using UnityEngine;
using System.Collections;

public class FireSpreadManager : MonoBehaviour
{
    [Header("Fuoco principale (già acceso)")]
    [SerializeField] private FireController mainFire;

    [Header("Oggetti fuoco da attivare in ordine")]
    [SerializeField] private GameObject[] firesToActivate;

    [Header("Intervallo di tempo tra un’attivazione e l’altra")]
    [SerializeField] private float delayBetweenActivations = 10f;

    private void Start()
    {
        // Avviamo la sequenza
        StartCoroutine(ActivateFiresInSequence());
    }

    private IEnumerator ActivateFiresInSequence()
    {
        // Se per qualche motivo il fuoco principale non è stato assegnato, fermiamoci
        if (mainFire == null)
        {
            Debug.LogError("Manca il riferimento al fuoco principale!");
            yield break;
        }

        // Cicliamo sugli oggetti da attivare uno dopo l’altro
        for (int i = 0; i < firesToActivate.Length; i++)
        {
            // Attendi il ritardo specificato
            yield return new WaitForSeconds(delayBetweenActivations);

            // Se il fuoco principale nel frattempo è stato spento, interrompi la propagazione
            if (mainFire.IsExtinguished)
            {
                Debug.Log("Fuoco principale estinto prima della propagazione. Mi fermo.");
                yield break;
            }

            // Altrimenti attiva il prossimo fuoco
            firesToActivate[i].SetActive(true);
            Debug.Log($"Fuoco {firesToActivate[i].name} attivato!");
        }
    }
}
