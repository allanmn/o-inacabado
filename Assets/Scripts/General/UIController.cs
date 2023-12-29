using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField]
    public GameObject goldAmount, itemMode;

    public int goldTotal = 0, goldRun = 0;

    void Awake()
    {
        goldAmount.GetComponent<TMP_Text>().text = goldRun.ToString();
    }

    public void AddToRunGold(int gold)
    {
        goldRun += gold;
        goldAmount.GetComponent<TMP_Text>().text = goldRun.ToString();
    }
}
