using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsListController : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public int id;

        public int hits;
        public float probability;
        public string name;
        public Sprite sprite;
    }

    public Item[] items;
}
