using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    private static NavMeshBaker _instance;
    public static NavMeshBaker Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

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


    //bool navMeshBuilt = false;

    public IEnumerator UpdateNavMeshAsync()
    {
        Debug.Log("<color=red>NavMeshBaker: Running UpdateNavMesh after 0.1sec</color>");
        yield return new WaitForSeconds(.1f);
        UpdateNavMesh();
    }

    public void UpdateNavMesh()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {

            //if (!navMeshBuilt)
            //{
            //    navMeshBuilt = true;
                Debug.Log("<color=red>NavMeshBaker: RemoveData + BuildNavMesh for " + surfaces[i].name + "</color>");
                surfaces[i].RemoveData();
                surfaces[i].BuildNavMesh();
            //} else
            //{
            //    Debug.Log("<color=red>NavMeshBaker: BuildNavMesh already run. Ignoring</color>");
            //}




            //if (!surfaces[i].navMeshData)
            //{
            //    Debug.Log("<color=red>NavMeshBaker: BuildNavMesh for " + surfaces[i].name + "</color>");
            //    surfaces[i].BuildNavMesh();

            //}
            //else
            //{

            //    Debug.Log("<color=red>NavMeshBaker: UpdateNavMesh for " + surfaces[i].name + "</color>");
            //    surfaces[i].UpdateNavMesh(surfaces[i].navMeshData);


            //}
        }


    }



}

