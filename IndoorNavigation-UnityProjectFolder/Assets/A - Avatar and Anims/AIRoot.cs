using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIRoot : MonoBehaviour
{
    Vector3 worldDeltaPosition = Vector3.zero;
    Vector3 position = Vector3.zero;
    NavMeshAgent agent;
    Animator animator;

    public Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updatePosition = false;
    }

    private void Update()
    {
        if (!target) return;

        worldDeltaPosition = agent.nextPosition - transform.position;

        if (worldDeltaPosition.magnitude > agent.radius)
            agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;
            agent.SetDestination(target.position);
    }

    private void OnAnimatorMove()
    {
        position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
    }
}
