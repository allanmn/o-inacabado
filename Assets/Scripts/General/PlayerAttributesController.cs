using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttributesController : MonoBehaviour
{
    UIController uiController;

    [SerializeField]
    public enum ItemEffect
    {
        None,
        GoldRush,
        SuperDamage,
        DefenceMode
    }

    [SerializeField]
    public ItemEffect itemEffect;

    IEnumerator itemStanceTimer;
    public bool itemStanceStatus = false;
    private float itemEffectDurationTime = 5f;

    void Awake()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
    }

    public void EnterItemStance(ItemEffect itemEffect)
    {
        uiController.itemMode.SetActive(true);
        this.itemEffect = itemEffect;
        itemStanceStatus = true;
        itemStanceTimer = ItemStanceTimer(itemEffectDurationTime);
        StartCoroutine(itemStanceTimer);
    }

    IEnumerator ItemStanceTimer(float itemEffectDurationTime)
    {
        yield return new WaitForSeconds(itemEffectDurationTime);
        itemStanceStatus = false;
        itemEffect = ItemEffect.None;
        uiController.itemMode.SetActive(false);
    }
}
