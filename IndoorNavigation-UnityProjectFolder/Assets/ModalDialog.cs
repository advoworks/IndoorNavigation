using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalDialog : MonoBehaviour
{

    public static ModalDialog Instance = null;

    public TextMeshProUGUI dialogText;
    public Button confirmButton;
    public Button cancelButton;
    public TextMeshProUGUI confirmText;
    public TextMeshProUGUI cancelText;


    private void Start()
    {
        Instance = this;

        //Set the modal dialog as disabled by default
        this.gameObject.SetActive(false);
    }

    public void SetDialogUnityEvent(DialogObjectUnityEvent d)
    {
        dialogText.text = d.dialogText;
        confirmText.text = d.confirmText;
        cancelText.text = d.cancelText;

        confirmButton.onClick.RemoveAllListeners(); //only removes non persistent. Will stil execute those events/listeners set via inspector. Becareful!
        confirmButton.onClick.AddListener(d.confirmEvent.Invoke);

    }

    public void SetDialogAction(DialogObjectUnityEvent d)
    {
        dialogText.text = d.dialogText;
        confirmText.text = d.confirmText;
        cancelText.text = d.cancelText;

        confirmButton.onClick.RemoveAllListeners(); //only removes non persistent. Will stil execute those events/listeners set via inspector. Becareful!
        confirmButton.onClick.AddListener(d.confirmEvent.Invoke);

    }

}

[System.Serializable]
public class DialogObjectUnityEvent
{
    public string dialogText;
    public string confirmText;
    public string cancelText;
    public UnityEvent confirmEvent;
}

