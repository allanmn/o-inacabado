using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    EnemiesListController enemiesList;
    IEnumerator spawnEnemy;
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    float spawnInterval = 2f, currentSpawnInterval = 2f, spawnMinX = -1.5f, spawnMaxX = 1.5f, spawnY = 6;

    public void Awake()
    {
        enemiesList = GameObject.Find("ScriptsController").GetComponent<EnemiesListController>();
        spawnEnemy = EnemySpawner(1f);
        StartCoroutine(spawnEnemy);
    }

    private IEnumerator EnemySpawner(float spawnInterval)
    {
        while (true)
        {
            SpawnEnemy(Random.Range(0, 6), Random.Range(1, 3), Random.Range(1, 3), new Vector2(Random.Range(spawnMinX, spawnMaxX), spawnY));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnEnemy(int enemyIdentifier, int enemyLevel, float enemyMoveSpeed, Vector2 coordinates)
    {
        var newEnemy = enemyPrefab;
        newEnemy.GetComponent<EnemyAttributes>().level = enemyLevel;
        newEnemy.GetComponent<EnemyAttributes>().identifier = enemyIdentifier;
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemiesList.enemies[enemyIdentifier].sprite;
        newEnemy.GetComponent<EnemyAttributes>().deathSprite = enemiesList.enemies[enemyIdentifier].deathSprite;
        newEnemy.GetComponent<SpriteRenderer>().name = enemiesList.enemies[enemyIdentifier].name;
        Instantiate(newEnemy, new Vector2(coordinates.x, coordinates.y), Quaternion.identity).transform.SetParent(GameObject.Find("EnemiesContainer").transform);
    }
}