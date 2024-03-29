using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        var newEnemy = Instantiate(enemyPrefab, new Vector2(coordinates.x, coordinates.y), Quaternion.identity);
        EnemyAttributes scp = newEnemy.GetComponent<EnemyAttributes>();
        scp.EnemyAttributesConstructor(enemyLevel);

        for (int i = 0; i <= 3; i++)
        {
            scp.drops.Add(i);
        }

        scp.identifier = enemyIdentifier;
        scp.deathSprite = enemiesList.enemies[enemyIdentifier].deathSprite;
        scp.name = enemiesList.enemies[enemyIdentifier].name;
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemiesList.enemies[enemyIdentifier].sprite;
        newEnemy.transform.SetParent(GameObject.Find("EnemiesContainer").transform);
    }
}