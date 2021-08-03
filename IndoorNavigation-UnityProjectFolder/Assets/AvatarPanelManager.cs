using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarPanelManager : MonoBehaviour
{
    public GameObject avatarPrefab;
    private GameObject avatar;


    RaycastHit downHit;
    Vector3 validatedDesPos;
    Vector3 validatedOriginPos;

    public void CreateAvatar()
    {
        

        

        

        //Get position to spawn Avatar on in front

        NavMeshHit hit;

        Vector3 avatarSpawnPoint = Camera.main.transform.position + (Camera.main.transform.forward * 2);
        Debug.Log("Camera.main.transform.position is " + Camera.main.transform.position);
        Debug.Log("avatarSpawnPoint is " + avatarSpawnPoint);

        if (NavMesh.SamplePosition(avatarSpawnPoint, out hit, 2.0f, NavMesh.AllAreas))
        {
            avatar = Instantiate(avatarPrefab, hit.position, Quaternion.identity, null);
            var lookPos = Camera.main.transform.position - avatar.transform.position;
            lookPos.y = 0;
            avatar.transform.rotation = Quaternion.LookRotation(lookPos);
            //var rotation = Quaternion.LookRotation(lookPos);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);


        }
        else
        {
            Debug.Log("NavMesh Sampleposition for avatar spawning failed");
        }

    }
}
