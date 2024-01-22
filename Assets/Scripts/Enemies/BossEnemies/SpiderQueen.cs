using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderQueen : EnemyBase
{
    [SerializeField] Enemy enemy;

    [SerializeField] public List<GameObject> legs;
    private float activateLegsTimer;
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

        for (int i = 0; i < transform.childCount; i++)
        {
            legs.Add(transform.GetChild(i).gameObject);
        }

        StartCoroutine(ActivateLegs(activateLegsTimer));
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
