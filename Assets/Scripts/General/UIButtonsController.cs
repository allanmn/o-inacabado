using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIButtonsController : MonoBehaviour
{
    GameObject gameMenu;

    GameObject inventoryMenu;

    public bool isPaused = false;

    public void ToggleMenu()
    {
        isPaused = !isPaused;
        gameMenu = GameObject.Find("GameMenu");
        if (isPaused)
        {
            gameMenu.transform.Find("Menu").gameObject.SetActive(true);
            gameMenu.transform.Find("Pause").gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            gameMenu.transform.Find("Menu").gameObject.SetActive(false);
            gameMenu.transform.Find("Pause").gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void ToggleCollectablesMenu()
    {
        GameObject.Find("Inventory").transform.Find("InventoryView").gameObject.SetActive(true);
    }

    public void BackButton()
    {
        // this.transform.parent.gameObject.SetActive(false);
    }
}
