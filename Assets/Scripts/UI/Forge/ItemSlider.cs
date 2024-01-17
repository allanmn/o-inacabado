using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlider : MonoBehaviour
{
    public SliderButton armorButton;
    public SliderButton weaponButton;
    public GameObject selectedItemContainer;
    public ItemsController itemsController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectWeapon()
    {
        weaponButton.SetDisabled(true);
        armorButton.SetDisabled(false);

        SetSelectedImage(itemsController.weapon.GetCurrentStage().sprite);

        itemsController.UpdatePriceList("weapon");
    }

    public void SelectArmor()
    {
        weaponButton.SetDisabled(false);
        armorButton.SetDisabled(true);

        SetSelectedImage(itemsController.armor.GetCurrentStage().sprite);
        itemsController.UpdatePriceList("armor");
    }

    private void SetSelectedImage(Sprite sprite)
    {
        if (selectedItemContainer != null && selectedItemContainer.TryGetComponent<Image>(out var image))
        {
            image.sprite = sprite;
        }
    }
}
