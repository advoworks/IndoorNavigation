using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

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

    public TextMeshProUGUI tmp;

    public Transform poiSofa;
    public Transform poiTVRoom;

    private Transform destination;

    public Transform player;

    public LineRenderer lr; //if navMeshPathDraw works, might not need lr anymore
    public NavmeshPathDraw navMeshPathDraw;

    private NavMeshPath path;

    //private bool showPath = false;
    //private float elapsed = 0.0f;

    [Header("Menu Panels")]
    public GameObject panelAvatar;
    public GameObject panelPOIs;
    public GameObject panelSearch;
    public GameObject panelSettings;
    public GameObject panelHome;


    [Header("POI prefab for POI list")]
    public GameObject poiListItemPrefab;
    [Header("POI list game object (should be in the scene)")]
    public GameObject poiList;
    [Header("POI objects in scene (Run time only)")]
    public GameObject[] poiObjects;

    //[Header("UI HUD Indicators")]
    //public TextMeshProUGUI tmpDestinationName;
    //public TextMeshProUGUI tmpDestinationDistance;


    private void Start()
    {
        // Hide all Menu Panels
        HideAllPanels();

        //Initialize path (to nothing?)
        path = new NavMeshPath();

        //Find all POI Objects in the scene and store in array, and populate the POI list
        poiObjects = GameObject.FindGameObjectsWithTag("POI");

        //Tell the POI Panel Manager to initialize itself with the list of POIs
        panelPOIs.GetComponent<PanelPOIManager>().GeneratePOIList(poiObjects);

        

    }

    public void ShowPathTo(GameObject poiObject)
    {
        navMeshPathDraw.destination = poiObject.transform;
        ModalDialogNaviCurrentDest.Instance.Show();
    }

    public void CancelNavigation()
    {
        navMeshPathDraw.destination = null;
        ModalDialogNaviCurrentDest.Instance.Hide();
    }

    public void UpdateDestinationNameAndDistance(string name, float distance)
    {
        ModalDialogNaviCurrentDest.Instance.dialogText.text = name;
        if (distance >= 0)
            ModalDialogNaviCurrentDest.Instance.remainingDistance.text = distance + "m";
        else
            ModalDialogNaviCurrentDest.Instance.remainingDistance.text = "Calculating...";
    }


    //public void ShowPathTo(GameObject poiObject)
    //{
    //    showPath = true;
    //    destination = poiObject.transform;
    //}



    //public void HidePath()
    //{
    //    lr.positionCount = 0;
    //    showPath = false;
    //}

    //private void Update()
    //{
    //    if (!showPath) return;

    //    // Update the way to the destination every second.
    //    elapsed += Time.deltaTime;
    //    if (elapsed > 1.0f)
    //    {
    //        elapsed -= 1.0f;

    //        NavMeshHit hit;
    //        if (NavMesh.SamplePosition(destination.position, out hit, 2.0f, NavMesh.AllAreas))
    //        {
    //            NavMesh.CalculatePath(player.transform.position, hit.position, NavMesh.AllAreas, path);
    //            //result = hit.position;
    //            //return true;
    //        } else
    //        {
    //            Debug.Log("Path not found to " + hit.position + ", abandon showing path");
    //            HidePath();
    //            return;
    //        }


    //    }

    //    //If we reach here we should be OK to draw the path
    //    Vector3[] corners = path.corners;
    //    lr.positionCount = corners.Length;
    //    lr.SetPositions(corners);

    //}


    //public void SetDestinationName(string name)
    //{
    //    tmpDestinationName.text = name;
    //}

    //public void SetDestinationDistance(float distance)
    //{
    //    tmpDestinationDistance.text = distance + "m";
    //}


    public void TargetFound()
    {
        tmp.text = "Target Found";
    }

    public void TargetLost()
    {
        tmp.text = "Target Lost";
    }



    public void MenuButtonAvatar()
    {
        panelAvatar.SetActive(true);
    }
    public void MenuButtonPOIs()
    {
        panelPOIs.SetActive(true);
    }
    public void MenuButtonSearch()
    {
        panelSearch.SetActive(true);
    }
    public void MenuButtonSettings()
    {
        panelSettings.SetActive(true);
    }
    public void MenuButtonHome()
    {
        //panelHome.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void HideAllPanels()
    {
        panelAvatar.SetActive(false);
        panelPOIs.SetActive(false);
        panelSearch.SetActive(false);
        panelSettings.SetActive(false);
        panelHome.SetActive(false);
    }

    public void DoNothing()
    {

    }


    
}
