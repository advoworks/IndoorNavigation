using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageTargetHandler : MonoBehaviour
{

    public DialogObjectUnityEvent homeDialog;
    public DialogObjectUnityEvent lobbyDialog;
    public DialogObjectUnityEvent scapeDialog;
    public DialogObjectUnityEvent magesDialog;
    public DialogObjectUnityEvent mallDialog;

    public GameObject loadingBarPanel;
    public Slider loadingBarSlider;
    

    private void Start()
    {
        loadingBarPanel.gameObject.SetActive(false);
    }

    public void QRScanned_Home()
    {

        ModalDialog.Instance.SetDialogUnityEvent(homeDialog);
        ModalDialog.Instance.gameObject.SetActive(true);

    }



    public void QRScanned_Lobby()
    {
        ModalDialog.Instance.SetDialogUnityEvent(lobbyDialog);
        ModalDialog.Instance.gameObject.SetActive(true);
    }

    public void QRScanned_Scape()
    {
        ModalDialog.Instance.SetDialogUnityEvent(scapeDialog);
        ModalDialog.Instance.gameObject.SetActive(true);
    }

    public void QRScanned_Mages()
    {
        ModalDialog.Instance.SetDialogUnityEvent(magesDialog);
        ModalDialog.Instance.gameObject.SetActive(true);
    }

    public void QRScanned_100am()
    {
        ModalDialog.Instance.SetDialogUnityEvent(mallDialog);
        ModalDialog.Instance.gameObject.SetActive(true);
    }


    public void Load_Home()
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadAsynchronously(1));

    }

    public void Load_Mages()
    {
        StartCoroutine(LoadAsynchronously(2));
    }

    public void Load_100am()
    {
        StartCoroutine(LoadAsynchronously(3));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingBarPanel.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBarSlider.value = progress;
            yield return null;
        }
    }

    //public void Load_Lobby()
    //{
    //    SceneManager.LoadScene(2);
    //}

    //public void Load_Scape()
    //{
    //    SceneManager.LoadScene(3);
    //}

    



    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
