using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectablePrice", menuName = "Collectables/CollectablePrice")]
public class CollectablePrice : ScriptableObject
{
    public Collectable collectable;

    public int amount;
}
