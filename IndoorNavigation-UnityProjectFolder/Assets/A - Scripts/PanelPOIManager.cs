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


    public enum PoiCategory
    {
        department,
        grocery,
        restaurant,
        clothing,
        accessory,
        pharmacy,
        pet,
        toy,
        speciality,
        thrift,
        services,
        kiosk,
        atm,
        taxi,
        train,
        entertainment
    }
    [System.Serializable]
    public class PoiCategoryImages
    {
        public PoiCategory poiCategory;
        public Sprite sprite;
    }

    public PoiCategoryImages[] poiCategoryImages;

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
            IOIHandler ioiHandler = poiGameObjects[i].GetComponent<IOIHandler>();

            poiGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ioiHandler.title;
            poiGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ioiHandler.desc;

            //Set POI Category Image on the list item
            for (int x = 0; x < poiCategoryImages.Length; x++)
            {
                if (poiCategoryImages[x].poiCategory == ioiHandler.poiCategory)
                {
                    Debug.Log("Found " + poiCategoryImages[x].poiCategory);
                    if (poiCategoryImages[x].sprite) // just in case we did not assign the sprite in the inspector
                    {
                        poiGameObject.transform.GetChild(2).GetComponent<Image>().sprite = poiCategoryImages[x].sprite;
                        break;
                    }
                        
                }
            }

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
        ModalDialogNaviYesNo.Instance.dialogText.text = poiObject.name;
        
        ModalDialogNaviYesNo.Instance.confirmButton.onClick.AddListener(delegate ()
        {
            Debug.Log("Confirm button clicked");

            ModalDialogNaviYesNo.Instance.Hide();

            GameController.Instance.ShowPathTo(poiObject);
            GameController.Instance.HideAllPanels();
        });

        ModalDialogNaviYesNo.Instance.cancelButton.onClick.AddListener(delegate ()
        {
            ModalDialogNaviYesNo.Instance.Hide();
        });

        ModalDialogNaviYesNo.Instance.Show();
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
