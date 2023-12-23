using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    [SerializeField]
    float maxHealth, currentHealth, moveSpeed, currentMoveSpeed;
    [SerializeField]
    int experience, gold;
    [SerializeField]
    public int level, identifier;
    [SerializeField]
    new string name;
    [SerializeField]
    bool isDead = false;

    public Sprite deathSprite;

    void Awake()
    {
        maxHealth = level * 5;
        currentHealth = maxHealth;
        moveSpeed = level * 2;
        currentMoveSpeed = moveSpeed;
    }

    private void Hit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hit(1);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            transform.position += new Vector3(0, -1 * moveSpeed * Time.deltaTime, 0);
        }
    }
}
