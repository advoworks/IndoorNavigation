using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IOIHandler : MonoBehaviour
{

    public TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp.gameObject.SetActive(false);
    }

    public void Display()
    {
        tmp.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tmp.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        Display();
    }
    private void OnTriggerExit(Collider other)
    {
        Hide();
    }
}
