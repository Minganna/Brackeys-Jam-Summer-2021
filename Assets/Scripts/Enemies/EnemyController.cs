using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    public float distanceToTarget=0.02f;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent)
        {
            Patrolling();
        }
    }
    void Patrolling()
    {
        agent.SetDestination(patrolPoints[currentPatrolPoint].position);

        if(agent.remainingDistance<=distanceToTarget)
        {
            currentPatrolPoint++;

            if(currentPatrolPoint>=patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }

            agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        }
    }
}
