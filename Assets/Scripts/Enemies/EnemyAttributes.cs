using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    PlayerAttributesController playerAttributesController;
    ItemsListController itemsList;

    UIController uiController;

    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    float maxHealth, currentHealth, moveSpeed, currentMoveSpeed;
    [SerializeField]
    public int level, identifier, experience, gold;
    [SerializeField]
    new string name;
    [SerializeField]
    bool isDead = false;
    [SerializeField]
    public List<int> drops;

    public Sprite deathSprite;

    void Awake()
    {
        //TEST ITEM DROPS
        // drops.Add(0);
        // drops.Add(1);
        // drops.Add(2);
        // drops.Add(3);

        playerAttributesController = GameObject.Find("ScriptsController").GetComponent<PlayerAttributesController>();

        itemsList = GameObject.Find("ScriptsController").GetComponent<ItemsListController>();

        uiController = GameObject.Find("UI").GetComponent<UIController>();

        gold = level * 2;
        maxHealth = level * 5;
        currentHealth = maxHealth;
        moveSpeed = level * 2;
        currentMoveSpeed = moveSpeed;
    }

    private void Hit(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        CheckItemsToDrop();
        EarnGold();
        Destroy(gameObject, .5f);
    }

    private void EarnGold()
    {
        //VERIFY IF ON ITEM MODE TO EARN MORE GOLD
        if (playerAttributesController.itemStanceStatus && playerAttributesController.itemEffect == PlayerAttributesController.ItemEffect.GoldRush)
        {
            uiController.AddToRunGold(gold * 2);
            Debug.Log("PICKED EXTRA GOLD FROM ACTIVE MODE");
        }
        else
        {
            uiController.AddToRunGold(gold);
        }
    }

    private void CheckItemsToDrop()
    {
        if (drops.Count > 0)
        {
            //PICKS RANDOM ITEM
            var itemIdentifier = Random.Range(0, drops.Count);
            var dropRateCheck = Random.Range(0.0f, 1.1f);

            if (dropRateCheck <= (itemsList.items[itemIdentifier].probability / 100f))
            {
                DropItem(itemIdentifier);
            }

        }
    }

    private void DropItem(int itemIdentifier)
    {
        var newItem = itemPrefab;
        newItem.GetComponent<ItemAttributes>().hits = itemsList.items[itemIdentifier].hits;
        newItem.GetComponent<SpriteRenderer>().sprite = itemsList.items[itemIdentifier].sprite;
        newItem.GetComponent<ItemAttributes>().identifier = itemIdentifier;
        newItem.GetComponent<ItemAttributes>().name = itemsList.items[itemIdentifier].name;
        Instantiate(newItem, new Vector2(transform.position.x, transform.position.y), Quaternion.identity).transform.SetParent(GameObject.Find("ItemsContainer").transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //SHOULD LOOK IN PLAYER CONTROLLER VARIABLES FOR THE VALUE
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
