using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

    public LineRenderer lr;

    private NavMeshPath path;

    private bool showPath = false;
    private float elapsed = 0.0f;

    [Header("Menu Panels")]
    public GameObject panelAvatar;
    public GameObject panelPOIs;
    public GameObject panelSearch;
    public GameObject panelSettings;
    public GameObject panelHome;


    private void Start()
    {
        HideAllPanels();
        path = new NavMeshPath();
    }
    public void ShowPathSofa()
    {
        showPath = true;
        destination = poiSofa;
    }

    public void ShowPathTVRoom()
    {
        showPath = true;
        destination = poiTVRoom;
    }


    public void HidePath()
    {
        lr.positionCount = 0;
        showPath = false;
    }

    private void Update()
    {
        if (!showPath) return;

        // Update the way to the destination every second.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(player.transform.position, destination.position, NavMesh.AllAreas, path);
        }
        
        Debug.Log("path.corners length is " + path.corners.Length);
        Vector3[] corners = path.corners;
        lr.positionCount = corners.Length;
        lr.SetPositions(corners);

    }



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
