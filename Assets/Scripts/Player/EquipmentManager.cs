using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } 
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Weapon currentWeapon;

    public delegate void OnWeaponChangedCallback();
    public OnWeaponChangedCallback onWeaponChangedCallback;

    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;

        onWeaponChangedCallback.Invoke();
    }
}
