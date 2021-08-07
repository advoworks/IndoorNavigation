using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickedUp : MonoBehaviour
{
    public UnityEvent ObjectPickUpColliderTriggered;

    private void OnTriggerEnter(Collider other)
    {       
        ObjectPickUpColliderTriggered.Invoke();
    }

    
}
