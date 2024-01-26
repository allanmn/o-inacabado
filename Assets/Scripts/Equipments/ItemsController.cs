using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemsController : MonoBehaviour
{
    public Armor armor;

    public Weapon weapon;

    public ItemSlider slider;

    public PriceList priceList;

    public TextMeshProUGUI upgradeChance;

    public TextMeshProUGUI maxLevelTxt;

    public UpgradeModal upgradeModal;

    public UpgradeButton upgradeButton;

    public AnimationContainer animationContainer;

    private string selectedType = "weapon";

    public ForgeData forgeData;

    public GameObject loadingModal;

    // Start is called before the first frame update
    void Start()
    {
        if (priceList != null)
        {
            priceList.itemsController = this;
        }

        if (forgeData != null)
        {
            ForgeData.onLoadComplete += OnLoadComplete;
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

    public void TryToUpgrade()
    {
        if (upgradeModal != null)
        {
            upgradeModal.modal.Show();
            animationContainer.ShowAnimation();

            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);

        if (animationContainer != null)
        {
            animationContainer.HideAnimation();

            yield return new WaitForSeconds(.5f);

            bool success = GetUpgradeResult();

            if (success)
            {
                if (selectedType == "weapon")
                {
                    weapon.currentStageIndex++;
                }
                else if (selectedType == "armor")
                {
                    armor.currentStageIndex++;
                }
            }
            else
            {
                if (selectedType == "weapon")
                {
                    weapon.GetNextStage().percentChance += 5;
                }
                else if (selectedType == "armor")
                {
                    armor.GetNextStage().percentChance += 5;
                }
            }

            upgradeModal.SetResult(success);

            UpdateData(selectedType);

            forgeData.SaveData();
        }
    }

    private bool GetUpgradeResult()
    {
        float percentChance = 0f;

        if (selectedType == "armor")
        {
            percentChance = ((float)armor.GetNextStage().percentChance) / 100;
        }
        else if (selectedType == "weapon")
        {
            percentChance = ((float)weapon.GetNextStage().percentChance) / 100;
        }

        var random = Random.value;

        if (random <= percentChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateData(string type)
    {
        selectedType = type;

            if (slider != null)
            {
                slider.SetSelectedImage(selectedType == "armor" ? armor.GetCurrentStage().sprite : weapon.GetCurrentStage().sprite);
            }

        if ((type == "armor" && armor.GetNextStage() == null) || (type == "weapon" && weapon.GetNextStage() == null))
        {
            priceList.gameObject.SetActive(false);
            upgradeChance.enabled = false;
            upgradeButton.SetEnabled(false);
            maxLevelTxt.gameObject.SetActive(true);

            return;
        } else
        {
            priceList.gameObject.SetActive(true);
            upgradeChance.enabled = true;
            upgradeButton.SetEnabled(true);
            maxLevelTxt.gameObject.SetActive(false);
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

            if (percentChance > 30 && percentChance < 70)
            {
                upgradeChance.color = Color.white;
            }
            else if (percentChance <= 30)
            {
                upgradeChance.color = Color.red;
            }
            else if (percentChance >= 70)
            {
                upgradeChance.color = Color.green;
            }
        }
    }

    public void DimissModal()
    {
        if (upgradeModal != null)
        {
            upgradeModal.modal.Hide();
            upgradeModal.resultText.gameObject.SetActive(false);
            upgradeModal.button.gameObject.SetActive(false);
        }

        if (upgradeButton != null)
        {
            upgradeButton.SetEnabled(true);
        }
    }

    public void OnLoadComplete()
    {
        LoadData();

        if (loadingModal != null)
        {
            loadingModal.SetActive(false);

            slider.gameObject.SetActive(true);
            priceList.transform.parent.gameObject.SetActive(true);
            upgradeButton.transform.parent.gameObject.SetActive(true);
        }
    }

    private void LoadData()
    {
        weapon.currentStageIndex = forgeData.data.weaponStageIndex;
        armor.currentStageIndex = forgeData.data.armorStageIndex;

        if (armor.GetNextStage() != null && forgeData.data.armorCurrentStageChance >= armor.GetNextStage().percentChance)
        {
            armor.GetNextStage().percentChance = forgeData.data.armorCurrentStageChance;
        }

        if (weapon.GetNextStage() != null && forgeData.data.weaponCurrentStageChance >= weapon.GetNextStage().percentChance)
        {
            weapon.GetNextStage().percentChance = forgeData.data.weaponCurrentStageChance;
        }

        UpdateData(selectedType);
    }
}
