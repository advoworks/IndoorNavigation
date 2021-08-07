using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static PanelPOIManager;

public class PickupHandler : MonoBehaviour
{

    
    public GameObject pickupObjectSpawnPoint; //This is where the object should spawn, and also contains the colliders for actual pickup
    public GameObject[] pickupObjectPrefabs;

    private GameObject pickupObject; //This is the instance of the spawned object populated at runtime



    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    


    private void OnTriggerEnter(Collider other)
    {
        if (!pickupObject)
        {
            int rand = Random.Range(0, pickupObjectPrefabs.Length);
            pickupObject = Instantiate(pickupObjectPrefabs[rand], pickupObjectSpawnPoint.transform.position, Quaternion.identity, pickupObjectSpawnPoint.transform );
        }

        Debug.Log(pickupObject.name + " set active true");
        pickupObject.SetActive(true);

        onTriggerEnter.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(pickupObject.name + " set active false");
        pickupObject.SetActive(false);
        onTriggerExit.Invoke();
    }


    public void ObjectPickedUp()
    {
        Destroy(gameObject);
        Scoreboard.Instance.AddScore();
    }
    

}
