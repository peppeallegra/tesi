using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private float range = 5.0f;
    [SerializeField] private Transform centrePoint;
    [SerializeField] private Transform evacuationPoint;

    private bool isEvacuating = false;
    private Vector3 lastRandomPoint;
    private bool hasValidPoint = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (!agent) Debug.LogError($"NavMeshAgent non trovato su {gameObject.name}");
        if (!animator) Debug.LogError($"Animator non trovato su {gameObject.name}");
    }

    void OnEnable()
    {
        FireAlarmButton.OnAlarmTriggered += HandleAlarm;
    }

    void OnDisable()
    {
        FireAlarmButton.OnAlarmTriggered -= HandleAlarm;
    }

    void Update()
    {
        if (isEvacuating)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                StopAgent();
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            MoveToRandomPoint();
        }
    }

    private void StopAgent()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        animator.SetBool("isWalking", false);
    }

    private void MoveToRandomPoint()
    {
        if (!hasValidPoint || Random.value < 0.1f) // Solo 10% di possibilità di ricalcolare
        {
            hasValidPoint = RandomPoint(centrePoint.position, range, out lastRandomPoint);
        }

        if (hasValidPoint)
        {
            agent.SetDestination(lastRandomPoint);
            animator.SetBool("isWalking", true);
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            randomPoint.y = center.y;

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void HandleAlarm(bool alarmActive)
    {
        isEvacuating = alarmActive;
        if (isEvacuating)
        {
            agent.SetDestination(evacuationPoint.position);
            animator.SetBool("isWalking", true);
        }
    }
}
