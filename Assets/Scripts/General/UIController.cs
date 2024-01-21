using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIController : MonoBehaviour
{
    PlayerAttributesController playerAttributesController;
    CollectablesListController collectablesList;

    [SerializeField] public GameObject goldAmount, itemMode, shield;

    [SerializeField] public int goldRun = 0, maxShieldHits, currentShieldHits;

    // [SerializeField]
    // public List<CollectablesListController.Collectable> collectables;
    [SerializeField]
    public Dictionary<int, int> collectables; //This keeps track of collectables quantity collected via COLLECTABLE_ID, COLLECTABLE_QUANTITY

    AudioListController audioListController;

    void Awake()
    {
        playerAttributesController = GameObject.Find("ScriptsController").GetComponent<PlayerAttributesController>();
        audioListController = GameObject.Find("AudioController").GetComponent<AudioListController>();
        audioListController.musicSource.PlayOneShot(audioListController.music[0].audioclip);

        collectables = new Dictionary<int, int>();

        collectablesList = GameObject.Find("ScriptsController").GetComponent<CollectablesListController>();

        //THIS GENERATES SOME ITEMS TO TEST PLAYER COLLECTABLES INVENTORY
        //TO ADD COLLECTABLES TO THE PLAYER INVENTORY, SIMPLY ADD/UPDATE THEM TO THE collectables DICTIONARY AND RUN INVENTORYCONTROLLER.UpdatePlayerInventory TO DISPLAY THE CORRECT AMMOUNTS

        //MUST ADD THE FUNCTION TO ADD COLLECTABLE TO PLAYER AND TO UPDATE COLLECTABLE
        for (int i = 0; i <= 3; i++)
        {
            //DICTIONARY
            collectables.Add(collectablesList.collectables[i].id, Random.Range(1, 501));

            //LIST
            // var newCollectable = collectablesList.collectables[i];
            // newCollectable.quantity = Random.Range(1, 25);
            // collectables.Add(newCollectable);
        }
        AddToRunGold(0);

        maxShieldHits = playerAttributesController.shieldHits;
        currentShieldHits = maxShieldHits;
        Debug.Log("CURRENT SHIELD HITS FROM AWAKE " + currentShieldHits);
        IncreaseShieldHits(maxShieldHits);
    }

    public void AddToRunGold(int amount)
    {
        goldRun += amount;
        goldAmount.GetComponent<TMP_Text>().text = goldRun.ToString();
    }

    public void setItemMode(string itemMode)
    {
        this.itemMode.GetComponent<TMP_Text>().text = itemMode;
    }

    public void IncreaseShieldHits(int amount)
    {
        if (currentShieldHits + amount < maxShieldHits)
        {
            currentShieldHits += amount;
        }
        else
        {
            currentShieldHits = maxShieldHits;
        }
        Debug.Log("SHIELD INCREASED " + currentShieldHits);
        shield.GetComponent<TMP_Text>().text = currentShieldHits.ToString();
    }

    public void DecreaseShieldHits(int amount)
    {
        audioListController.effectsSource.PlayOneShot(audioListController.effects[8].audioclip);
        if (currentShieldHits - amount <= 0)
        {
            currentShieldHits = 0;
            shield.GetComponent<TMP_Text>().text = currentShieldHits.ToString();
            GameOver();
        }
        else
        {
            currentShieldHits -= amount;
        }
        shield.GetComponent<TMP_Text>().text = currentShieldHits.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("InfiniteMode");
        Time.timeScale = 1;
    }
}
