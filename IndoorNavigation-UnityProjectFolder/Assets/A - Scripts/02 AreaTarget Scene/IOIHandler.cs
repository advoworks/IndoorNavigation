using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static PanelPOIManager;

public class IOIHandler : MonoBehaviour
{

    public Canvas canvas;

    public TextMeshProUGUI tmpTitle;
    public TextMeshProUGUI tmpDesc;

    public string title;
    public string desc;
    public PoiCategory poiCategory;

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    // Start is called before the first frame update
    void Start()
    {
        //tmpTitle.gameObject.SetActive(false);
        tmpTitle.text = title;

        //tmpDesc.gameObject.SetActive(false);
        tmpDesc.text = desc;

        canvas.gameObject.SetActive(false);
    }

    public void Display()
    {
        //tmpTitle.gameObject.SetActive(true);
        //tmpDesc.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        //tmpTitle.gameObject.SetActive(false);
        //tmpDesc.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Display();
        canvas.gameObject.SetActive(true);
        onTriggerEnter.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        //Hide();
        onTriggerExit.Invoke();
    }


}
