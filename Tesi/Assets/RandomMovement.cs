using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Necessario per il NavMesh

public class RandomMovement : MonoBehaviour 
{
    public NavMeshAgent agent;
    public float range = 5.0f; // Raggio dell'area in cui può muoversi
    public Transform centrePoint; // Centro dell'area in cui deve muoversi

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Controllo iniziale per verificare che il NavMeshAgent sia presente
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent non trovato sul GameObject " + gameObject.name);
        }
    }

    void Update()
    {
        // Se l'agente è fermo o ha raggiunto la destinazione, trova un nuovo punto
        if (agent.pathPending)
        {
            Debug.Log("Percorso in calcolo...");
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance || agent.remainingDistance < 1.0f) 
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                // Evita di scegliere punti troppo vicini alla posizione attuale
                if (Vector3.Distance(agent.transform.position, point) > 2.0f) 
                {
                    Debug.Log("Nuova destinazione trovata: " + point);
                    agent.SetDestination(point);
                }
                else
                {
                    Debug.Log("Punto troppo vicino, ignorato.");
                }
            }
            else
            {
                Debug.Log("Nessuna destinazione valida trovata!");
            }
        }
    }

    // Funzione per trovare un punto casuale valido sulla NavMesh
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++) // Prova fino a 10 volte
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            randomPoint.y = center.y; // Mantieni l'altezza costante per evitare problemi

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas)) 
            {
                Debug.DrawRay(hit.position, Vector3.up * 2, Color.blue, 1.0f); // Debug visivo
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
