using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{

    public static Scoreboard Instance = null;

    public GameObject mainPanel;
    //public TextMeshProUGUI dialogText;
    public TextMeshProUGUI scoreText;
    //public Button confirmButton;
    //public Button cancelButton;

    private int currentScore = 0;


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


    public void AddScore()
    {
        Show();
        currentScore++;
        scoreText.text = currentScore + " / 100";
    }
}
