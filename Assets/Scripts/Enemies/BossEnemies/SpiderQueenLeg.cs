using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderQueenLeg : EnemyBase
{
    [SerializeField] Enemy enemy;
    void Awake()
    {
        enemyName = enemy.enemyName;
        moveSpeed = enemy.moveSpeed;
        currentMoveSpeed = moveSpeed;
        maxHealth = enemy.health;
        currentHealth = maxHealth;
        damage = enemy.damage;
        currentDamage = damage;
        isImmune = true;
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isImmune)
        {
            Hit(1);
            Debug.Log(currentHealth);
        }
    }
}