using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PriceList : MonoBehaviour
{
    public GameObject prefab;

    public List<GameObject> prices;

    public ItemsController itemsController;

    public Sprite GoldSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (prices == null)
        {
            prices = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateList(string type)
    {
        ClearPriceList();

        if (type == "armor")
        {
            var stage = itemsController.armor.GetNextStage();

            if (stage) {
                foreach (var item in stage.collectables)
                {
                    NewPrefab(item);
                }

                AddGoldPrice(stage);
            }
        } else if (type == "weapon")
        {
            var stage = itemsController.weapon.GetNextStage();

            if (stage)
            {
                foreach (var item in stage.collectables)
                {
                    NewPrefab(item);
                }

                AddGoldPrice(stage);
            }
        }
    }

    private void NewPrefab(CollectablePrice item)
    {
        GameObject listItem = (GameObject) PrefabUtility.InstantiatePrefab(prefab);

        if (listItem.TryGetComponent<CollectablePriceController>(out var controller))
        {
            controller.image.sprite = item.collectable.sprite;
            controller.priceAmount.text = item.amount.ToString();
        }

        if (TryGetComponent<RectTransform>(out var rectTransform))
        {
            listItem.transform.SetParent(transform);
            listItem.transform.localScale = Vector2.one;
        }

        prices.Add(listItem);
    }

    private void AddGoldPrice(Stage item)
    {
        GameObject listItem = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        if (listItem.TryGetComponent<CollectablePriceController>(out var controller))
        {
            controller.image.sprite = GoldSprite;
            controller.priceAmount.text = item.goldPrice.ToString();
        }

        if (TryGetComponent<RectTransform>(out var rectTransform))
        {
            listItem.transform.SetParent(transform);
            listItem.transform.localScale = Vector2.one;
        }

        prices.Add(listItem);
    }

    private void ClearPriceList()
    {
        foreach (var item in prices)
        {
            Destroy(item);
        }

        prices.Clear();
    }
}
