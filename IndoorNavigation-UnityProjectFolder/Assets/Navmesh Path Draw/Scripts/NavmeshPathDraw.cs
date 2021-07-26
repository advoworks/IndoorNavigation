using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]

public class NavmeshPathDraw : MonoBehaviour
{
    

    public Transform destination;
    public bool recalculatePath = true;
    public float recalculationTime = 0.1f;
    public float yOffset; // Kev Added


    NavMeshPath path;
    LineRenderer lr;

    float time = 0f;
    bool stopped = false;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        path = new NavMeshPath();

        if(lr.materials.Length == 0){
            lr.material = Resources.Load("material/path_mat", typeof(Material)) as Material;
        }

        Draw();
    }
    
    //draw the path
    public void Draw()
    {
        if (destination == null) return;
        stopped = false;
        
        RaycastHit downHit;
        Vector3 validatedDesPos;
        Vector3 validatedOriginPos;

        /*GET THE NAVMESH POSITION BELOW DESTINATION AND ORIGIN IN ORDER TO PRINT THE PATH*/
        //validate destination position
        if (Physics.Raycast(destination.position, -Vector3.up, out downHit, Mathf.Infinity)) {
            validatedDesPos = new Vector3(destination.position.x, downHit.transform.position.y, destination.position.z);
        }else{
            validatedDesPos = destination.position;
        }

        //validate origin position
        if (Physics.Raycast(transform.position, -Vector3.up, out downHit, Mathf.Infinity)) {
            validatedOriginPos = new Vector3(transform.position.x, downHit.transform.position.y, transform.position.z);
        }else{
            validatedOriginPos = transform.position;
        }


        //START - KEV MOD

        //NavMesh.CalculatePath(validatedOriginPos, validatedDesPos, NavMesh.AllAreas, path);
        //Vector3[] corners = path.corners;

        //lr.positionCount = corners.Length;
        //lr.SetPositions(corners);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(validatedDesPos, out hit, 2.0f, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(validatedOriginPos, hit.position, NavMesh.AllAreas, path);
            Vector3[] corners = path.corners;

            if (yOffset != 0f)
            {
                for (int i = 0; i < corners.Length; i++)
                {
                    corners[i].y += yOffset;
                }
            }

            lr.positionCount = corners.Length;
            lr.SetPositions(corners);


        }
        else
        {
            Debug.Log("No path was found, setting lr position count to 0");
            lr.positionCount = 0;
        }

        //Now calculate the distance
        float distance = 0;
        for (int i = 0; i < lr.positionCount - 1; i++)
        {
            distance += (lr.GetPosition(i + 1) - lr.GetPosition(i)).magnitude;
        }
        GameController.Instance.SetDestinationName(destination.name);
        GameController.Instance.SetDestinationDistance(distance);

        //END - KEV MOD
    }

    //stop drawing the path
    public void Stop()
    {
        stopped = true;
        lr.positionCount = 0;
    }

    //recalculate the route ONCE every frame if enabled
    void Update()
    {
        if (!recalculatePath) return;
        if (!stopped) time += Time.deltaTime;

        if(time >= recalculationTime && !stopped){
            time = 0f;
            Draw();
        }
    }
}
