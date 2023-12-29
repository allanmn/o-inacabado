using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
            //CHECK PLAYER DAMAGE
            Hit(1);
        }
    }

    private void Hit(int damage)
    {
        currentHits -= damage;
        if (currentHits <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            PickItem();
            Debug.Log("PLAYER PICKED ITEM");
        }
        else
        {
            Debug.Log("PLAYER Hit ITEM ONCE - " + currentHits);
        }
    }

    private void PickItem()
    {
        if (playerAttributesController.itemStanceStatus == false)
        {
            //HARDCODED GOLD RUSH
            Debug.Log("GoldRush Activated");
            playerAttributesController.EnterItemStance(PlayerAttributesController.ItemEffect.GoldRush);
        }
        else
        {
            Debug.Log("ALREADY STANCE MODE");
        }
        Destroy(gameObject, 0f);
    }
}
