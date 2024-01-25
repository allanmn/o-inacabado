using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyBase
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
        isImmune = false;
    }

    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other, 1);
        HitDestroyer(other, currentDamage);

    }
}
