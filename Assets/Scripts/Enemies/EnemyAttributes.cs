using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public PlayerAttributesController playerAttributesController;
    ItemsListController itemsList;

    UIController uiController;

    [SerializeField] GameObject itemPrefab;

    [SerializeField] GameObject healthBar;

    [SerializeField]
    float maxHealth, currentHealth, moveSpeed, currentMoveSpeed;
    [SerializeField]
    public int level, identifier, experience, gold, damage;
    [SerializeField]
    new string name;
    [SerializeField]
    public string named;
    [SerializeField]
    bool isDead = false;
    [SerializeField]
    public List<int> drops;

    public Sprite deathSprite;

    AudioListController audioListController;

    private IEnumerator enemyHitSoundTimer;
    private bool isPlayingHitSound = false;
    private float timeBetweenHits = .2f;

    public void EnemyAttributesConstructor(int level)
    {
        audioListController = GameObject.Find("AudioController").GetComponent<AudioListController>();

        gold = level * 2;
        maxHealth = level * 5;
        currentHealth = maxHealth;
        moveSpeed = level * 2;
        currentMoveSpeed = moveSpeed;
        // damage = Random.Range(1, level);
        damage = 1;

        healthBar.GetComponent<StatusBar>().SetStatusBar(currentHealth, maxHealth);
    }

    void Awake()
    {
        playerAttributesController = GameObject.Find("ScriptsController").GetComponent<PlayerAttributesController>();

        itemsList = GameObject.Find("ScriptsController").GetComponent<ItemsListController>();

        uiController = GameObject.Find("UI").GetComponent<UIController>();
    }

    public void Hit(int damage)
    {
        if (!isPlayingHitSound)
        {
            isPlayingHitSound = true;
            audioListController.effectsSource.PlayOneShot(audioListController.effects[Random.Range(6, 7)].audioclip);
            StartCoroutine(EnemyHitSoundTimer(timeBetweenHits));
        }
        currentHealth -= damage;
        healthBar.GetComponent<StatusBar>().UpdateStatusBar(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            audioListController.effectsSource.PlayOneShot(audioListController.effects[5].audioclip, .5f);
            Destroy();
        }
    }

    IEnumerator EnemyHitSoundTimer(float timeBetweenHits)
    {
        yield return new WaitForSeconds(timeBetweenHits);
        isPlayingHitSound = false;
    }

    public void Destroy()
    {
        healthBar.SetActive(false);
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
            var itemIdentifier = Random.Range(0, drops.Count);
            var dropRateCheck = Random.Range(0.0f, 1.1f);

            if (dropRateCheck <= (itemsList.items[itemIdentifier].probability / 100f))
            {
                DropItem(itemIdentifier + 1);
            }
        }
    }

    private void DropItem(int itemIdentifier)
    {
        var newItem = Instantiate(itemPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        newItem.transform.SetParent(GameObject.Find("ItemsContainer").transform);
        ItemAttributes scp = newItem.GetComponent<ItemAttributes>();
        scp.ItemAttributesConstructor(itemIdentifier);
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
