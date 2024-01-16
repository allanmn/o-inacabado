using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponContainer : MonoBehaviour
{
    public GameObject itemImage;

    public GameObject itemTitle;

    public GameObject itemPrice;

    public OldEquipment equipment;

    public void SetupComponent()
    {
        if (equipment != null)
        {
            // set image
            if (itemImage != null)
            {
                if (itemImage.TryGetComponent<Image>(out var image))
                {
                    if (equipment.TryGetComponent<Image>(out var equipmentImage))
                    {
                        image.sprite = equipmentImage.sprite;
                    }
                }
            }

            if (itemTitle != null)
            {
                if (itemTitle.TryGetComponent<TMPro.TextMeshProUGUI>(out var text))
                {
                    text.text = equipment.equipmentName;
                }
            }

            if (itemPrice != null)
            {
                if (itemPrice.TryGetComponent<TMPro.TextMeshProUGUI>(out var text))
                {
                    text.text = equipment.price.ToString();
                }
            }
        }
    }
}
