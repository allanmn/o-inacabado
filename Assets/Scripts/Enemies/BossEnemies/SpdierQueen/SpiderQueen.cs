using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class SpiderQueen : EnemyBase
{
    [SerializeField] Enemy enemy;

    [SerializeField] GameObject egg;

    [SerializeField] public List<GameObject> legs;
    private float activateLegsTimer, behaveTimer, spawnEggTimer;

    [SerializeField]
    public enum Stance
    {
        Idle,
        Attacking
    }

    [SerializeField] public Stance stance;

    private bool moveRight;
    private bool moveUp;

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



        activateLegsTimer = 5;
        behaveTimer = 5;
        spawnEggTimer = 1.5f;

        for (int i = 0; i < transform.childCount; i++)
        {
            legs.Add(transform.GetChild(i).gameObject);
        }

        StartCoroutine(ActivateLegs(activateLegsTimer));

        stance = Stance.Idle;

        StartCoroutine(Behave(behaveTimer));
        StartCoroutine("Move");
    }

    IEnumerator Behave(float behaveTimer)
    {
        stance = Stance.Idle;
        yield return new WaitForSeconds(behaveTimer);
        stance = Stance.Attacking;
        Attack();
    }

    IEnumerator SpawnEgg(float spawnEggTimer)
    {
        yield return new WaitForSeconds(spawnEggTimer);
    }

    private void Attack()
    {
        var spawnEggs = Random.Range(2, 5);

        for (var i = 0; i < spawnEggs; i++)
        {
            Instantiate(egg, new Vector2(Random.Range(-2f, 2f), Random.Range(1.5f, -2f)), Quaternion.identity, GameObject.Find("EnemiesContainer").transform);
            StartCoroutine(SpawnEgg(spawnEggTimer));
        }
        StartCoroutine(Behave(behaveTimer));
    }

    IEnumerator ActivateLegs(float activateLegsTimer)
    {
        for (int i = 0; i < legs.Count; i++)
        {
            if (legs[i] != null)
            {
                legs[i].GetComponent<EnemyBase>().isImmune = true;
                legs[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            }

        }
        FindLeg();

        yield return new WaitForSeconds(activateLegsTimer);
        if (legs.Count > 0)
        {
            StartCoroutine(ActivateLegs(activateLegsTimer));
        }
        else
        {
            isImmune = false;
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
    }

    private void FindLeg()
    {
        if (legs.Count > 0)
        {
            var legId = Random.Range(0, legs.Count);
            if (legs[legId] != null)
            {
                legs[legId].GetComponent<EnemyBase>().isImmune = false;
                legs[legId].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            }
            else
            {
                legs.Remove(legs[legId]);
                FindLeg();
            }
        }
    }


    IEnumerator Move()
    {
        if (isImmune)
        {
            float x = 0;
            if (transform.position.x == 0)
            {

                var random = Random.Range(0f, 1f);
                if (random < .5f)
                {
                    x = -.5f;
                }
                else
                {
                    x = .5f;
                }
            }
            else
            {
                if (transform.position.x > 0)
                {
                    x = Random.Range(-.5f, 0f);
                    moveRight = false;
                }
                else
                {
                    x = Random.Range(0f, .5f);
                    moveRight = true;
                }
            }
            if (moveRight)
            {
                while (transform.position.x < x)
                {
                    transform.position += new Vector3(1 * .5f * Time.deltaTime, 0, 0);

                    if (moveUp)
                    {
                        transform.position += new Vector3(0, 1 * 1.5f * Time.deltaTime, 0);
                        if (transform.position.y >= 5f)
                        {
                            moveUp = false;
                        }
                    }
                    else
                    {
                        if (transform.position.y > 3.5f)
                        {
                            transform.position -= new Vector3(0, 1 * 1.5f * Time.deltaTime, 0);
                        }
                        else
                        {
                            moveUp = true;
                        }
                    }
                    yield return null;
                }
            }
            else
            {
                while (transform.position.x > x)
                {
                    transform.position -= new Vector3(1 * .5f * Time.deltaTime, 0, 0);

                    if (moveUp)
                    {
                        transform.position += new Vector3(0, 1 * 1.5f * Time.deltaTime, 0);
                        if (transform.position.y >= 5f)
                        {
                            moveUp = false;
                        }
                    }
                    else
                    {
                        if (transform.position.y > 3.5f)
                        {
                            transform.position -= new Vector3(0, 1 * 1.5f * Time.deltaTime, 0);
                        }
                        else
                        {
                            moveUp = true;
                        }
                    }
                    yield return null;
                }
            }


            yield return new WaitForSeconds(5);
            StartCoroutine("Move");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //PLAYER FDAMAGE ON BASE SCRIPT
        Hit(other, 1);
    }
}