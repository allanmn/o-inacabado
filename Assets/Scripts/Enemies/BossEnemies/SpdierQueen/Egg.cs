using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : EnemyBase
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject spider;

    private float spawnSpiderTimer;

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

        float minSpawnSpiderTimer = 1f;
        float maxSpawnSpiderTimer = 5f;

        StartCoroutine(SpawnSpider(Random.Range(minSpawnSpiderTimer, maxSpawnSpiderTimer)));
    }

    IEnumerator SpawnSpider(float spawnSpiderTimer)
    {
        yield return new WaitForSeconds(spawnSpiderTimer);
        Instantiate(spider, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, GameObject.Find("EnemiesContainer").transform);
        StartCoroutine(SpawnSpider(spawnSpiderTimer));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other, 1);
    }
}
