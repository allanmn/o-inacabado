using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int maxLevel = 3;

    public int currentLevel = 1;

    public bool alreadyBought = false;

    public bool isEquipped = false;

    public string equipmentName = "Armor";

    public int price = 0;
}
