using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesListController : MonoBehaviour
{
    [System.Serializable]
    public class Collectable
    {
        public int id;
        public int dropRate;
        public string name;
        public Sprite sprite;
    }

    public Collectable[] collectables;
}
