using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour
{

    [Header("Search Field")]
    public TextMeshProUGUI searchFilter;
    public TMP_InputField searchInputField;

    [Header("Search Button, OnClick assigned programmatically")]
    public Button searchButton;

    [Header("POI List Item Prefab")]
    public GameObject avatarListItemPrefab;

    [Header("POI List GameObject in Scene UI")]
    public GameObject poiListInScene;


    
    [System.Serializable]
    public class AvatarDefinition
    {
        public string avatarTitle;
        public string avatarDesc;
        public GameObject avatarPrefab;
        public Sprite sprite;
    }

    public AvatarDefinition[] avatarDefinitions;


    private GameObject avatar;

    private void Start()
    {
        searchButton.onClick.AddListener(FilterListByText);

    }


    public void GenerateAvatarList()
    {
        GameObject avatarListItem;

        //Delete 1st list item from the editor
        Destroy(poiListInScene.transform.GetChild(0).gameObject);

        //GEnerate the list
        for (int i = 0; i < avatarDefinitions.Length; i++)
        {
            avatarListItem = Instantiate(avatarListItemPrefab, poiListInScene.transform);
            avatarListItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = avatarDefinitions[i].avatarTitle;
            avatarListItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = avatarDefinitions[i].avatarDesc;
            avatarListItem.transform.GetChild(2).GetComponent<Image>().sprite = avatarDefinitions[i].sprite;
            
            var index = i;
            avatarListItem.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                AvatarListItemClicked(avatarDefinitions[index]);
            });
        }
    }

    private void AvatarListItemClicked(AvatarDefinition avatarDefinition)
    {

        //Display modal dialog and confirm navigation to that object
        ModalDialogNaviYesNo.Instance.mainText.text = "Create Avatar:";
        ModalDialogNaviYesNo.Instance.subjectText.text = avatarDefinition.avatarTitle;// poiObject.name;

        ModalDialogNaviYesNo.Instance.confirmButton.onClick.RemoveAllListeners();
        ModalDialogNaviYesNo.Instance.confirmButton.onClick.AddListener(delegate ()
        {
            Debug.Log("Confirm button clicked");

            ModalDialogNaviYesNo.Instance.Hide();

            CreateAvatar(avatarDefinition);

            GameController.Instance.HideAllPanels();
        });

        ModalDialogNaviYesNo.Instance.cancelButton.onClick.RemoveAllListeners();
        ModalDialogNaviYesNo.Instance.cancelButton.onClick.AddListener(delegate ()
        {
            ModalDialogNaviYesNo.Instance.Hide();
        });

        ModalDialogNaviYesNo.Instance.Show();
    }






    public void CreateAvatar(AvatarDefinition avatarDefinition)
    {


        //Get position to spawn Avatar on in front

        NavMeshHit hit;

        Vector3 avatarSpawnPoint = Camera.main.transform.position + (Camera.main.transform.forward * 2);
        Debug.Log("Camera.main.transform.position is " + Camera.main.transform.position);
        Debug.Log("avatarSpawnPoint is " + avatarSpawnPoint);

        if (NavMesh.SamplePosition(avatarSpawnPoint, out hit, 2.0f, NavMesh.AllAreas))
        {
            avatar = Instantiate(avatarDefinition.avatarPrefab, hit.position, Quaternion.identity, null);
            var lookPos = Camera.main.transform.position - avatar.transform.position;
            lookPos.y = 0;
            avatar.transform.rotation = Quaternion.LookRotation(lookPos);
       
        }
        else
        {
            Debug.Log("NavMesh Sampleposition for avatar spawning failed");
        }

    }


    private void FilterListByText()
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
