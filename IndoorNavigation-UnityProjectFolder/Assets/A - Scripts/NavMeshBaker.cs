using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{

    public bool bakeUponStart = true;
    public bool autoRefresh = true;

    public float recalculationTime = 2f;
    public NavMeshSurface[] surfaces;
    private float time = 0f;

    private void Start()
    {
        if (!bakeUponStart) return;
        UpdateNavMesh();
    }

    private void Update()
    {

        if (!autoRefresh) return;

        time += Time.deltaTime;

        if (time >= recalculationTime)
        {
            time = 0f;
            
            UpdateNavMesh();
        }
    }

    //private void BakeNavMesh()
    //{
    //    for (int i = 0; i < surfaces.Length; i++)
    //    {
    //        Debug.Log("NavMeshBaker: BuildNavMesh for " + surfaces[i].name);
    //        surfaces[i].BuildNavMesh();            
    //    }
    //}

    public void UpdateNavMesh()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            if (!surfaces[i].navMeshData)
            {
                Debug.Log("<color=red>NavMeshBaker: BuildNavMesh for " + surfaces[i].name + "</color>");
                surfaces[i].BuildNavMesh();                
            }
            else
            {
                Debug.Log("<color=red>NavMeshBaker: UpdateNavMesh for " + surfaces[i].name + "</color>");
                surfaces[i].UpdateNavMesh(surfaces[i].navMeshData);
            }
        }

        //for (int i = 0; i < surfaces.Length; i++)
        //{
        //    surfaces[i].UpdateNavMesh(surfaces[i].navMeshData);

        //}
    }
}
