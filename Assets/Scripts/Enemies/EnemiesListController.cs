using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesListController : MonoBehaviour
{
    [System.Serializable]
    public class Enemy
    {
        public Sprite sprite;
        public Sprite deathSprite;
        public string name;
    }

    public Enemy[] enemies;
}
