using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Necessario per il NavMesh

public class RandomMovement : MonoBehaviour 
{
    public NavMeshAgent agent;
    public float range = 5.0f; // Raggio dell'area in cui può muoversi
    public Transform centrePoint; // Centro dell'area di movimento
    public Transform evacuationPoint; // Punto di raccolta

    private bool isEvacuating = false; // Controlla se è in evacuazione

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent non trovato sul GameObject " + gameObject.name);
        }
        
        // Iscriviamo questo NPC all'evento dell'allarme
        FireAlarmButton.OnAlarmTriggered += HandleAlarm;
    }

    void Update()
    {
        if (isEvacuating) return; // Se è in evacuazione, non muoversi casualmente

        if (agent.pathPending) return;

        if (agent.remainingDistance <= agent.stoppingDistance || agent.remainingDistance < 1.0f) 
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                if (Vector3.Distance(agent.transform.position, point) > 2.0f) 
                {
                    agent.SetDestination(point);
                }
            }
        }
    }

    // Funzione per trovare un punto casuale sulla NavMesh
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++) 
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            randomPoint.y = center.y;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas)) 
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    // Gestisce l'attivazione dell'allarme
    void HandleAlarm(bool alarmActive)
    {
        if (alarmActive)
        {
            isEvacuating = true;
            agent.SetDestination(evacuationPoint.position);
            Debug.Log(gameObject.name + " si sta dirigendo verso il punto di raccolta!");
        }
        else
        {
            isEvacuating = false;
        }
    }

    private void OnDestroy()
    {
        // Disiscriviamo l'NPC dall'evento quando viene distrutto
        FireAlarmButton.OnAlarmTriggered -= HandleAlarm;
    }
}
