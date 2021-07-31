using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]

public class NavmeshPathDraw : MonoBehaviour
{

    [SerializeField]
    private Transform destination;

    public bool recalculatePath = true;
    public float recalculationTime = 0.1f;
    public Vector3 lineOffset; // Kev Added
    public GameObject destinationMarkerPrefab;
    private GameObject destinationMarker;
    
    

    NavMeshPath path;
    LineRenderer lr;

    float time = 0f;
    bool stopped = false;


    //START - KEV MOD
    public void SetDestination(Transform dest)
    {
        destination = dest;
        lr.positionCount = 0;

        //destinationMarker.SetActive(true);
        //destinationMarker.transform.position = dest.position;
        
    }

    public void ClearDestination()
    {
        destination = null;
        lr.positionCount = 0;

        destinationMarker.SetActive(false);
    }

    //END - KEV MOD

    void Awake()
    {

        destinationMarker = Instantiate(destinationMarkerPrefab);
        destinationMarker.SetActive(false);

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
        //START - KEV Commented

        //if (Physics.Raycast(destination.position, -Vector3.up, out downHit, Mathf.Infinity)) {
        //    validatedDesPos = new Vector3(destination.position.x, downHit.transform.position.y, destination.position.z);          
        //}
        //else
        //{
        //    validatedDesPos = destination.position;
        //}

        ////validate origin position
        //if (Physics.Raycast(transform.position, -Vector3.up, out downHit, Mathf.Infinity)) {
        //    validatedOriginPos = new Vector3(transform.position.x, downHit.transform.position.y, transform.position.z);            
        //}
        //else{
        //    validatedOriginPos = transform.position;            
        //}


        

        //NavMesh.CalculatePath(validatedOriginPos, validatedDesPos, NavMesh.AllAreas, path);
        //Vector3[] corners = path.corners;

        //lr.positionCount = corners.Length;
        //lr.SetPositions(corners);

        //END - KEV Commented

        //START - KEV MOD

        if (Physics.Raycast(destination.position, -Vector3.up, out downHit, Mathf.Infinity))
        {
            validatedDesPos = new Vector3(destination.position.x, downHit.transform.position.y, destination.position.z);
            Debug.Log("<color=LightBlue>NavmeshPathDraw: Destination downward raycast SUCCESS</color>");
        }
        else
        {
            validatedDesPos = destination.position;
            Debug.Log("<color=LightBlue>NavmeshPathDraw: Destination downward raycast DID NOT WORK, using original position</color>");
        }

        //validate origin position
        if (Physics.Raycast(transform.position, -Vector3.up, out downHit, Mathf.Infinity))
        {
            validatedOriginPos = new Vector3(transform.position.x, downHit.transform.position.y, transform.position.z);
            Debug.Log("<color=LightBlue>NavmeshPathDraw: Origin downward raycast SUCCESS</color>");
        }
        else
        {
            validatedOriginPos = transform.position;
            Debug.Log("<color=LightBlue>NavmeshPathDraw: Origin downward raycast DID NOT WORK, using original position</color>");
        }

        bool pathFound = false;
        pathFound = NavMesh.CalculatePath(validatedOriginPos, validatedDesPos, NavMesh.AllAreas, path);

        if (pathFound)
        {
            Debug.Log("<color=LightBlue>Path found</color>");
            Vector3[] corners = path.corners;

            
            for (int i = 0; i < corners.Length; i++)
            {
                corners[i] += lineOffset;
            }
            

            lr.positionCount = corners.Length;
            lr.SetPositions(corners);

            //Animated the material?
            //https://stackoverflow.com/questions/57364629/how-to-animate-line-renderer-tiled-texture-in-unity
            lr.material.SetTextureOffset("_MainTex", Vector2.left * Time.time);

            //Set destination marker
            destinationMarker.SetActive(true);
            destinationMarker.transform.position = validatedDesPos + lineOffset;
        }
        else
        {
            Debug.Log("<color=LightBlue>No path was found, setting lr position count to 0</color>");
            lr.positionCount = 0;
        }

        //NavMeshHit hit;
        
        //if (NavMesh.SamplePosition(validatedDesPos, out hit, 2.0f, NavMesh.AllAreas))
        //{
        //    pathFound = NavMesh.CalculatePath(validatedOriginPos, hit.position, NavMesh.AllAreas, path);
        //    Vector3[] corners = path.corners;

        //    if (yOffset != 0f)
        //    {
        //        for (int i = 0; i < corners.Length; i++)
        //        {
        //            corners[i].y += yOffset;
        //        }
        //    }

        //    lr.positionCount = corners.Length;
        //    lr.SetPositions(corners);


        //}
        //else
        //{
        //    Debug.Log("No path was found, setting lr position count to 0");
        //    lr.positionCount = 0;
        //}

        float distance = 0;

        if (pathFound)
        {
            //Now calculate the distance
         
            for (int i = 0; i < lr.positionCount - 1; i++)
            {
                distance += (lr.GetPosition(i + 1) - lr.GetPosition(i)).magnitude;
            }

            GameController.Instance.UpdateDestinationNameAndDistance(destination.name, distance);
        } else
        {
            GameController.Instance.UpdateDestinationNameAndDistance(destination.name, -1);
        }
        
        

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
