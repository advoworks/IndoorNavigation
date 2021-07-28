using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelPOIManager : MonoBehaviour
{

    [Header("Search Field")]
    public TextMeshProUGUI searchFilter;
    public TMP_InputField searchInputField;

    [Header("Search Button, OnClick assigned programmatically")]
    public Button searchButton;

    [Header("POI List Item Prefab")]
    public GameObject poiListItemPrefab;

    [Header("POI List GameObject in Scene UI")]
    public GameObject poiListInScene;

    private void Start()
    {
        searchButton.onClick.AddListener(FilterPOIListByText);

       
    }


    public void GeneratePOIList(GameObject[] poiGameObjects)
    {
        GameObject poiGameObject;
        for (int i = 0; i < poiGameObjects.Length; i++)
        {
            poiGameObject = Instantiate(poiListItemPrefab, poiListInScene.transform);
            poiGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = poiGameObjects[i].GetComponent<IOIHandler>().title;
            poiGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = poiGameObjects[i].GetComponent<IOIHandler>().desc;

            //poiGameObject.GetComponent<POIListItem>().poiObject = poiGameObjects[i];

            var index = i;
            poiGameObject.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                poiListItemClicked(poiGameObjects[index]);
            });
        }
    }

    private void poiListItemClicked(GameObject poiObject)
    {
       
        //Display modal dialog and confirm navigation to that object
        ModalDialogNaviScene.Instance.dialogText.text = "Navigate to " + poiObject.name;
        
        ModalDialogNaviScene.Instance.confirmButton.onClick.AddListener(delegate ()
        {
            Debug.Log("Confirm button clicked");

            ModalDialogNaviScene.Instance.Hide();

            GameController.Instance.ShowPathTo(poiObject);
            GameController.Instance.HideAllPanels();
        });

        ModalDialogNaviScene.Instance.cancelButton.onClick.AddListener(delegate ()
        {
            ModalDialogNaviScene.Instance.Hide();
        });

        ModalDialogNaviScene.Instance.Show();
    }

    
    private void FilterPOIListByText()
    {
        //This is not an efficient way of searching, but this is a prototype
        //This method run through each list item and searches its text component
        //A better way would be to search an array of classes in memory and only display the remaining results in UI
        Debug.Log("Filtering list now by text: " + searchFilter.text);
        for (int i = 0; i < poiListInScene.transform.childCount; i++)
        {
            string listItemTitle = poiListInScene.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

            //Use the parent InputField, not the child textmeshpro
            //https://forum.unity.com/threads/textmesh-pro-ugui-hidden-characters.505493/
            //if (listItemTitle.Contains(searchInputField.text))
            if (listItemTitle.IndexOf(searchInputField.text, System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Debug.Log("'" + listItemTitle + "' CONTAINS the string: '" + searchFilter.text + "'");
                //List ITem matches search filter, ensure it is displayed
                poiListInScene.transform.GetChild(i).gameObject.SetActive(true);
            } else
            {
                Debug.Log("'" + listItemTitle + "' DOES NOT CONTAINS the string: '" + searchFilter.text + "'");
                poiListInScene.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }
}
