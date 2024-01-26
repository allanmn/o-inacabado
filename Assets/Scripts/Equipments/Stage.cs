using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Stage : MonoBehaviour
{
    public int valueToAdd;

    public Sprite sprite;

    public int goldPrice;

    public int percentChance;

    public List<CollectablePrice> collectables;


    // Start is called before the first frame update
    void Start()
    {
        if (collectables == null)
        {
            collectables = new List<CollectablePrice>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
