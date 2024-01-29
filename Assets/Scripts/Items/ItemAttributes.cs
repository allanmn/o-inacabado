using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    PlayerAttributesController playerAttributesController;
    ItemsListController itemsList;
    UIController uiController;

    [SerializeField]
    public int identifier;

    public int hits, currentHits;
    [SerializeField]
    public bool isDestroyed = false;
    AudioListController audioListController;

    private IEnumerator invulnerabilityTimer;
    private bool isInvulnerable = true;
    private float invulnerabilityTime = .25f;

    public void ItemAttributesConstructor(int identifier)
    {
        this.identifier = identifier;

        itemsList = GameObject.Find("ScriptsController").GetComponent<ItemsListController>();

        audioListController = GameObject.Find("AudioController").GetComponent<AudioListController>();

        if (identifier == 4)
        {
            audioListController.effectsSource.PlayOneShot(audioListController.effects[2].audioclip);
        }
        else
        {
            audioListController.effectsSource.PlayOneShot(audioListController.effects[0].audioclip);
        }

        hits = itemsList.items[identifier - 1].hits;
        currentHits = hits;

        GetComponent<SpriteRenderer>().sprite = itemsList.items[identifier - 1].sprite;
        name = itemsList.items[identifier - 1].name;

        playerAttributesController = GameObject.Find("ScriptsController").GetComponent<PlayerAttributesController>();

        uiController = GameObject.Find("UI").GetComponent<UIController>();
    }

    void Awake()
    {
        SpawnForce();
        invulnerabilityTimer = InvulnerabilityTimer(invulnerabilityTime);
        StartCoroutine(invulnerabilityTimer);
    }

    IEnumerator InvulnerabilityTimer(float invulnerabilityTime)
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private void SpawnForce()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1.0f, 1.1f), 1 * 3.5f), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInvulnerable)
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

    private void Hit(int damage)
    {
        currentHits -= damage;
        if (currentHits <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            audioListController.effectsSource.PlayOneShot(audioListController.effects[1].audioclip);
            PickItem();
        }
    }

    private void PickItem()
    {
        if (playerAttributesController.itemStanceStatus == false || identifier == 4 || identifier == 3)
        {
            Debug.Log("ENTERED STANCE MODE " + identifier);
            switch (identifier)
            {
                case 1:
                    playerAttributesController.EnterItemStance(PlayerAttributesController.ItemEffect.GoldRush);
                    break;
                case 2:
                    playerAttributesController.EnterItemStance(PlayerAttributesController.ItemEffect.SuperDamage);
                    break;
                case 3:
                    // playerAttributesController.EnterItemStance(PlayerAttributesController.ItemEffect.Shield);
                    uiController.IncreaseShieldHits(1);
                    break;
                case 4:
                    uiController.GameOver();
                    break;
            }

        }
        else
        {
            Debug.Log("ALREADY STANCE MODE");
        }
        Destroy(gameObject, 0f);
    }
}
