using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectable", menuName = "Collectables/Collectable")]
public class Collectable : ScriptableObject
{
    public int id;
    public int dropRate;
    public string collectableName;
    public Sprite sprite;
}
