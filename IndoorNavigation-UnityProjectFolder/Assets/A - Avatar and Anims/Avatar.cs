using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Avatar : MonoBehaviour
{
    AIRoot AI;
    Animator anim;
    public float stopDistance;
    public float hitpoints = 100f;
    public UnityEvent onDeath;

    void Start()
    {
        AI = GetComponent<AIRoot>();
        anim = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        GameController.Instance.NavigationPathFoundAction += StartNavigation;
    }

    private void OnDisable()
    {
        GameController.Instance.NavigationPathFoundAction -= StartNavigation;
    }

    private void StartNavigation(Transform destination)
    {
        AI.target = destination;
    }

    



    void Update()
    {
        if (!AI.target) return;

        float remainingDistance = Vector3.Distance(AI.transform.position, AI.target.position);
        
        if (remainingDistance < stopDistance)
        {
            //Debug.Log("Idle Trigger, Distance " + remainingDistance);
            AnimSetBool("idle");
            
            AI.target = null;
        }
        else {
            //Debug.Log("Walk Trigger, Distance " + remainingDistance);
            AnimSetBool("walk");
            
        }
    }


    void AnimSetBool(string param)
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            anim.SetBool(parameter.name, false);
        }

        anim.SetBool(param, true);
    }

    

    

   
}
