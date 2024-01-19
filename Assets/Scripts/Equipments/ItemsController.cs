using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsController : MonoBehaviour
{
    public Armor armor;

    public Weapon weapon;

    public ItemSlider slider;

    public PriceList priceList;

    public TextMeshProUGUI upgradeChance;

    public Modal upgradeModal;

    public UpgradeButton upgradeButton;

    public AnimationContainer animationContainer;

    private string selectedType = "weapon";

    // Start is called before the first frame update
    void Start()
    {
        if (priceList != null)
        {
            priceList.itemsController = this;
        }

        if (slider != null)
        {
            slider.itemsController = this;

            slider.SelectWeapon();
        }

        if (upgradeButton != null)
        {
            upgradeButton.controller = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryToUpgrade()
    {
        if (upgradeModal != null)
        {
            upgradeModal.Show();

            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);

        if (animationContainer != null)
        {
            animationContainer.HideAnimation();
        }
    }

    public void UpdateData(string type)
    {
        selectedType = type;

        if ((type == "armor" && armor.GetNextStage() == null) || (type == "weapon" && weapon.GetNextStage() == null)) {
            priceList.enabled = false;
            upgradeChance.enabled = false;

            return;
        }

        if (priceList != null)
        {
            priceList.UpdateList(type);
        }

        if (upgradeChance != null)
        {
            var percentChance = 0;
            if (type == "armor")
            {
                percentChance = armor.GetNextStage().percentChance;
            }
            else if (type == "weapon")
            {
                percentChance = weapon.GetNextStage().percentChance;
            }

            upgradeChance.text = percentChance + "%";

            if (percentChance <= 30)
            {
                upgradeChance.color = Color.red;
            } else if (percentChance >= 70)
            {
                upgradeChance.color = Color.green;
            }
        }
    }
}
