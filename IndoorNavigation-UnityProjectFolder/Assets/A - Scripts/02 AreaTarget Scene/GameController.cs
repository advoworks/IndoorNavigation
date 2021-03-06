using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public Action<Transform> NavigationPathFoundAction;

    public void NavigationPathFound(Transform x)
    {
        //Debug.Log("GameController: NavigationPathFound called to notify Avatar");
        if (NavigationPathFoundAction != null)
            NavigationPathFoundAction(x);
    }

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
    public GameObject bottomPanelAccess;
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

        //Tell the Avatar Manager to initialize itself with the list of Avatars
        panelAvatar.GetComponent<AvatarManager>().GenerateAvatarList();

    }

    public void ShowPathTo(GameObject poiObject)
    {
        //navMeshPathDraw.destination = poiObject.transform;
        navMeshPathDraw.SetDestination(poiObject.transform);
        ModalDialogNaviCurrentDest.Instance.Show();
        bottomPanelAccess.gameObject.SetActive(false);
    }

    public void CancelNavigation()
    {
        //navMeshPathDraw.destination = null;
        navMeshPathDraw.ClearDestination();
        ModalDialogNaviCurrentDest.Instance.Hide();
        bottomPanelAccess.gameObject.SetActive(true);
    }

    public void UpdateDestinationNameAndDistance(string name, float distance)
    {
        ModalDialogNaviCurrentDest.Instance.dialogText.text = name;
        if (distance >= 0)
            ModalDialogNaviCurrentDest.Instance.remainingDistance.text = distance + "m";
        else
            ModalDialogNaviCurrentDest.Instance.remainingDistance.text = "Calculating...";
    }

    
    public void TargetFound()
    {
        tmp.text = "Target Found";
        //NavMeshBaker.Instance.UpdateNavMesh();
        StartCoroutine(NavMeshBaker.Instance.UpdateNavMeshAsync());
     
    }

    public void TargetLost()
    {
        tmp.text = "Target Lost";
    }



    public void MenuButtonAvatar()
    {
        panelAvatar.SetActive(true);
        bottomPanelAccess.gameObject.SetActive(false);
    }
    public void MenuButtonPOIs()
    {
        panelPOIs.SetActive(true);
        bottomPanelAccess.gameObject.SetActive(false);
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

        bottomPanelAccess.gameObject.SetActive(true);
    }

    public void DoNothing()
    {

    }


    
}
