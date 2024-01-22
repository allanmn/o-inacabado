using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemy")]
public class Enemy : ScriptableObject
{
    public GameObject enemy;
    public string enemyName;
    public int moveSpeed;
    public int health;
    public int damage;
    public Sprite sprite;
    public Sprite deathSprite;
}
