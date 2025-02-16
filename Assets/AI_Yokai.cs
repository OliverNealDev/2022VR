using UnityEngine;
using UnityEngine.AI;

public class AI_Yokai : MonoBehaviour
{
    public enum State { Patrol, Chase, Investigate }
    public State currentState = State.Patrol;
    private NavMeshAgent agent;
    public float patrolRange = 20f;
    public float waitDelayThreshold = 10f;
    private float waitDelay = 0f;
    private Vector3 patrolDestination;
    public float detectionDistance = 50f;
    public float viewAngle = 90f;
    public float proximityDistance = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //SetNewPatrolDestination();
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Patrol:
                PatrolState();
                break;
            case State.Chase:
                ChaseState();
                break;
            case State.Investigate:
                InvestigateState();
                break;
        }

        DetectPlayer();
    }

    void PatrolState()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitDelay += Time.fixedDeltaTime;
            if (waitDelay >= waitDelayThreshold)
            {
                waitDelay = 0f;
                //SetNewPatrolDestination();
            }
        }
    }

    void ChaseState()
    {
        
    }

    void InvestigateState()
    {
        
    }

    void SetNewPatrolDestination()
    {
        patrolDestination = GetRandomNavmeshLocation(patrolRange);
        agent.SetDestination(patrolDestination);
    }

    Vector3 GetRandomNavmeshLocation(float minDistance)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float randomDistance = minDistance + Random.Range(0f, minDistance);
        Vector3 randomPoint = transform.position + new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)) * randomDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, minDistance, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
    
    public bool DetectPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            return false;
        }
    
        Vector3 directionToPlayer = playerObj.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
    
        if (distanceToPlayer <= proximityDistance)
        {
            Debug.Log("Player detected via proximity!");
            return true;
        }
    
        if(distanceToPlayer > detectionDistance)
        {
            return false;
        }
    
        directionToPlayer.Normalize();
    
        if (Vector3.Dot(transform.forward, directionToPlayer) < Mathf.Cos(viewAngle * Mathf.Deg2Rad))
        {
            return false;
        }
    
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionDistance))
        {
            if (hit.collider.gameObject == playerObj)
            {
                Debug.Log("Player detected via raycast!");
                return true;
            }
        }
    
        return false;
    }
}