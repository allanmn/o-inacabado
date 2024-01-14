using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    CollectablesListController collectablesList;
    UIController UIController;
    [SerializeField] GameObject collectablePrefab;

    public GameObject[] collectableItems;

    int counter;

    void Start()
    {
        UIController = GameObject.Find("UI").GetComponent<UIController>();

        collectablesList = GameObject.Find("ScriptsController").GetComponent<CollectablesListController>();

        //SETS THE POINTER ARRAY THE SAME SIZE AS THE TOTAL LIST
        collectableItems = new GameObject[collectablesList.collectables.Length];

        Transform collectablesListContainer = GameObject.Find("CollectablesList").transform;

        counter = 0;
        foreach (CollectablesListController.Collectable collectable in collectablesList.collectables)
        {
            var newCollectable = Instantiate(collectablePrefab, collectablesListContainer);

            CollectableAttributes scp = newCollectable.GetComponent<CollectableAttributes>();

            scp.CollectableAttributesConstructor(collectable.sprite, collectable.name);

            collectableItems[counter] = newCollectable;
            counter++;
        }

        UpdatePlayerInventory();
    }

    public void UpdatePlayerInventory()
    {
        //CANNOT RUN COUNTER, MUST RUN FULL DICTIONARY FOR ALL AVAILABLE KEYS AND UPDATE THE RESPECTIVE GAMEOBJECT
        counter = 1;
        foreach (GameObject collectableItem in collectableItems)
        {
            if (UIController.collectables.ContainsKey(counter))
            {
                Debug.Log("CONTAINES KEY " + counter + " THE VALUE IS " + UIController.collectables[counter]);
                collectableItem.GetComponent<CollectableAttributes>().quantity.SetText(UIController.collectables[counter].ToString());
            }
            counter++;
        }
    }

    //MUST ADD FUNCTIONS TO UPDATE SINGLE INVENTORY ITEM
}
