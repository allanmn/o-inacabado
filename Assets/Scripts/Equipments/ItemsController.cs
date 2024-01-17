using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    public Armor armor;

    public Weapon weapon;

    public ItemSlider slider;

    public PriceList priceList;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePriceList(string type)
    {
        if (priceList != null)
        {
            priceList.UpdateList(type);
        }
    }
}
