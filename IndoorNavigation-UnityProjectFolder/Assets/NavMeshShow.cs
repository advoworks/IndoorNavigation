using UnityEngine;
using UnityEngine.AI;

public class NavMeshShow : MonoBehaviour
{

	public bool showUponStart = true;
	public bool autoRefresh = true;
	
	public float recalculationTime = 2f;

	Mesh mesh;
	float time = 0f;

    void Start()
    {
		
		if (!showUponStart) return;
		CreateNavMeshDisplay();
    }

    void Update()
	{
		if (!autoRefresh) return;

		time += Time.deltaTime;

		if (time >= recalculationTime)
		{
			time = 0f;
			CreateNavMeshDisplay();
		}
	}

	// Generates the NavMesh shape and assigns it to the MeshFilter component.
	public void CreateNavMeshDisplay()
	{

		
		// NavMesh.CalculateTriangulation returns a NavMeshTriangulation object.
		NavMeshTriangulation meshData = NavMesh.CalculateTriangulation();

		// Create a new mesh and chuck in the NavMesh's vertex and triangle data to form the mesh.
		mesh = new Mesh();
		mesh.name = "Mesh Display from NavMesh";
		mesh.vertices = meshData.vertices;
		mesh.triangles = meshData.indices;

		Debug.Log("<color=green>NavMeshShow: CreateNavMeshDisplay " + meshData.vertices.Length + " vertices " + meshData.indices.Length + " indices </color>");

		// Assigns the newly-created mesh to the MeshFilter on the same GameObject.
		GetComponent<MeshFilter>().mesh = mesh;
	}
}