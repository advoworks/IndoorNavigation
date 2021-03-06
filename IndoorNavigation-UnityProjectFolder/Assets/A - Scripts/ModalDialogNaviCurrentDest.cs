using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalDialogNaviCurrentDest : MonoBehaviour
{

    public static ModalDialogNaviCurrentDest Instance = null;

    public GameObject mainPanel;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI remainingDistance;
    public Button confirmButton;
    public Button cancelButton;



    private void Start()
    {
        Instance = this;

        //Set the modal dialog as disabled by default
        mainPanel.SetActive(false);
    }

    public void Show()
    {
        mainPanel.SetActive(true);
    }

    public void Hide()
    {
        mainPanel.SetActive(false);
    }

    public void Cancel()
    {
        GameController.Instance.CancelNavigation();
    }
}
