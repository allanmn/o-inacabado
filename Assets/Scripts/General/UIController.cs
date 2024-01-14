using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class UIController : MonoBehaviour
{

    CollectablesListController collectablesList;

    [SerializeField]
    public GameObject goldAmount, itemMode;

    public int goldTotal = 0, goldRun = 0;

    // [SerializeField]
    // public List<CollectablesListController.Collectable> collectables;
    [SerializeField]
    public Dictionary<int, int> collectables; //This keeps track of collectables quantity collected via COLLECTABLE_ID, COLLECTABLE_QUANTITY

    AudioListController audioListController;

    void Awake()
    {
        audioListController = GameObject.Find("AudioController").GetComponent<AudioListController>();
        audioListController.musicSource.PlayOneShot(audioListController.music[0].audioclip);

        collectables = new Dictionary<int, int>();

        collectablesList = GameObject.Find("ScriptsController").GetComponent<CollectablesListController>();

        //THIS GENERATES SOME ITEMS TO TEST PLAYER COLLECTABLES INVENTORY
        //TO ADD COLLECTABLES TO THE PLAYER INVENTORY, SIMPLY ADD/UPDATE THEM TO THE collectables DICTIONARY AND RUN INVENTORYCONTROLLER.UpdatePlayerInventory TO DISPLAY THE CORRECT AMMOUNTS

        //MUST ADD THE FUNCTION TO ADD COLLECTABLE TO PLAYER AND TO UPDATE COLLECTABLE
        for (int i = 0; i <= 4; i++)
        {
            //DICTIONARY
            Debug.Log(collectablesList.collectables[i].id);
            collectables.Add(collectablesList.collectables[i].id, Random.Range(1, 501));

            //LIST
            // var newCollectable = collectablesList.collectables[i];
            // newCollectable.quantity = Random.Range(1, 25);
            // collectables.Add(newCollectable);
        }
        goldAmount.GetComponent<TMP_Text>().text = goldRun.ToString();
    }

    public void AddToRunGold(int gold)
    {
        goldRun += gold;
        goldAmount.GetComponent<TMP_Text>().text = goldRun.ToString();
    }

    public void setItemMode(string itemMode)
    {
        this.itemMode.GetComponent<TMP_Text>().text = itemMode;
    }
}
