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
    public string named;
    [SerializeField]
    bool isDead = false;
    [SerializeField]
    public List<int> drops;

    public Sprite deathSprite;

    public void EnemyAttributesConstructor(int level)
    {
        gold = level * 2;
        maxHealth = level * 5;
        currentHealth = maxHealth;
        moveSpeed = level * 2;
        currentMoveSpeed = moveSpeed;
    }

    void Awake()
    {
        playerAttributesController = GameObject.Find("ScriptsController").GetComponent<PlayerAttributesController>();

        itemsList = GameObject.Find("ScriptsController").GetComponent<ItemsListController>();

        uiController = GameObject.Find("UI").GetComponent<UIController>();
    }

    private void Hit(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
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
        newItem.GetComponent<ItemAttributes>().identifier = itemIdentifier + 1;
        newItem.GetComponent<ItemAttributes>().name = itemsList.items[itemIdentifier].name;
        Instantiate(newItem, new Vector2(transform.position.x, transform.position.y), Quaternion.identity).transform.SetParent(GameObject.Find("ItemsContainer").transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerAttributesController.itemStanceStatus && playerAttributesController.itemEffect == PlayerAttributesController.ItemEffect.SuperDamage)
            {
                Hit(playerAttributesController.currentDamage * 2);
                Debug.Log("EXTRA SLASH DAMAGE");
            }
            else
            {
                Hit(playerAttributesController.currentDamage);
            }
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
