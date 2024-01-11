using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    PlayerAttributesController playerAttributesController;

    [SerializeField]
    public int identifier;

    public int hits, currentHits;
    [SerializeField]
    public bool isDestroyed = false;

    void Awake()
    {
        currentHits = hits;
        playerAttributesController = GameObject.Find("ScriptsController").GetComponent<PlayerAttributesController>();

        SpawnForce();
    }

    private void SpawnForce()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1.0f, 1.1f), 1 * 3.5f), ForceMode2D.Impulse);
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

    private void Hit(int damage)
    {
        currentHits -= damage;
        if (currentHits <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            PickItem();
        }
    }

    private void PickItem()
    {
        if (playerAttributesController.itemStanceStatus == false || identifier == 4)
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
                    playerAttributesController.EnterItemStance(PlayerAttributesController.ItemEffect.Shield);
                    break;
                case 4:
                    Time.timeScale = 0;
                    SceneManager.LoadScene("InfiniteMode");
                    Time.timeScale = 1;
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
