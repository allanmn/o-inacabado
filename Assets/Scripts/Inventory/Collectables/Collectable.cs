using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectable", menuName = "Collectables/Collectable")]
public class Collectable : ScriptableObject
{
    public int id;
    public int dropRate;
    public string itemName;
    public Sprite sprite;
}
