using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageTargetHandler : MonoBehaviour
{

    public DialogObject homeDialog;
    public DialogObject lobbyDialog;
    public DialogObject scapeDialog;

    public void QRScanned_Home()
    {

        ModalDialog.Instance.SetDialog(homeDialog);
        ModalDialog.Instance.gameObject.SetActive(true);

    }



    public void QRScanned_Lobby()
    {
        ModalDialog.Instance.SetDialog(lobbyDialog);
        ModalDialog.Instance.gameObject.SetActive(true);
    }

    public void QRScanned_Scape()
    {
        ModalDialog.Instance.SetDialog(scapeDialog);
        ModalDialog.Instance.gameObject.SetActive(true);
    }


    public void Load_Home()
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadAsynchronously(1));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            yield return null;
        }
    }

    public void Load_Lobby()
    {
        SceneManager.LoadScene(2);
    }

    public void Load_Scape()
    {
        SceneManager.LoadScene(3);
    }


    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
