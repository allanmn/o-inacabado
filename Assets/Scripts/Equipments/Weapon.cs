using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Weapon : Equipment
{
    public int damagePerLevel = 5;
}
