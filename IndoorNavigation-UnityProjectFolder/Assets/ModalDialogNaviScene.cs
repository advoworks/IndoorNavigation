using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalDialogNaviScene : MonoBehaviour
{

    public static ModalDialogNaviScene Instance = null;

    public GameObject mainPanel;
    public TextMeshProUGUI dialogText;
    public Button confirmButton;
    public Button cancelButton;
    public TextMeshProUGUI confirmText;
    public TextMeshProUGUI cancelText;


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

}
