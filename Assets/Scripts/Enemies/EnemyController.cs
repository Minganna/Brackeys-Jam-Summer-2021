using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    public float distanceToTarget=0.02f;
    public float waitAtPoint=2.0f;
    public float chaseRange;
    public float attackRange = 1.0f;
    public float timeBetweenAttacks=2.0f;
    float attackCounter;
    float waitAtCounter;

    public NavMeshAgent agent;

    Animator anim;

    public enum AIState
    {
        isIdle,
        isPatrolling,
        isChasing,
        isAttacking,
        isDeath
    };

    public AIState currentState;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        waitAtCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent&&currentState !=AIState.isDeath)
        {
            
            Patrolling();
        }
    }
    void Patrolling()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        switch (currentState)
        {
            case AIState.isIdle:
                {
                    anim.SetBool("IsMoving", false);
                    if (waitAtCounter>0)
                    {
                        waitAtCounter -= Time.deltaTime;
                    }
                    else
                    {
                        currentState = AIState.isPatrolling;
                        agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    }
                    if (distanceToPlayer <= chaseRange)
                    {
                        currentState = AIState.isChasing;
                    }
                    break;
                }
            case AIState.isPatrolling:
                {
                    

                    if (agent.remainingDistance <= distanceToTarget)
                    {
                        currentPatrolPoint++;

                        if (currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }

                        currentState = AIState.isIdle;
                        waitAtCounter = waitAtPoint;
                    }
                    if (distanceToPlayer <= chaseRange)
                    {
                        currentState = AIState.isChasing;
                    }
                    anim.SetBool("IsMoving", true);
                    break;
                }
            case AIState.isChasing:
                {
                    anim.SetBool("IsMoving", true);
                    agent.SetDestination(PlayerController.instance.transform.position);
                    if(distanceToPlayer<=attackRange)
                    {
                        currentState = AIState.isAttacking;
                        anim.SetTrigger("Attack");
                        anim.SetBool("IsMoving", false);
                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;

                        attackCounter = timeBetweenAttacks;
                    }

                    if(distanceToPlayer>chaseRange)
                    {
                        currentState = AIState.isIdle;
                        waitAtCounter = waitAtPoint;
                        agent.velocity = Vector3.zero;
                        agent.SetDestination(transform.position);
                    }
                    break;
                }
            case AIState.isAttacking:
                {
                    transform.LookAt(PlayerController.instance.transform,Vector3.up);
                    transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
                    attackCounter -= Time.deltaTime;
                    if(attackCounter<=0)
                    {
                        if(distanceToPlayer<attackRange)
                        {
                            anim.SetTrigger("Attack");
                            attackCounter = timeBetweenAttacks;
                        }
                        else
                        {
                            currentState = AIState.isIdle;
                            waitAtCounter = waitAtPoint;
                            agent.isStopped=false;
                        }
                    }
                    break;
                }
                
                
    }

   
    }

    public void PlayDeath()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        anim.SetTrigger("isDeath");
        currentState = AIState.isDeath;
        agent.velocity = Vector3.zero;
        agent.SetDestination(transform.position);
    }
}
