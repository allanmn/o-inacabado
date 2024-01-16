using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    public Armor armor;

    public Weapon weapon;

    public ItemSlider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.itemsController = this;

        slider.SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
