using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ForgeItemsController : MonoBehaviour
{
    public List<Weapon> weapons;

    public List<Armor> armors;

    public GameObject weaponContainerPrefab;

    public GameObject gridContainer;

    public TabGroup tabGroup;

    TabButton selectedTab;

    List<GameObject> containers;

    // Start is called before the first frame update
    void Start()
    {
        if (weapons == null)
        {
            weapons = new List<Weapon>();
        }

        if (armors == null)
        {
            armors = new List<Armor>();
        }

        if (containers == null)
        {
            containers = new List<GameObject>();
        }

        if (tabGroup != null)
        {
            tabGroup.itemsController = this;
            selectedTab = tabGroup.selectedTab;
        }

        PopulateList();
    }

    public void TabChanged(TabButton button)
    {
        if (selectedTab != button)
        {
            selectedTab = button;

            RemoveContainers();

            PopulateList();
        }
    }

    void PopulateList()
    {
        if (selectedTab.type == TabButton.Type.Weapon)
        {
            foreach (Weapon weapon in weapons)
            {
                GameObject listItem = (GameObject)PrefabUtility.InstantiatePrefab(weaponContainerPrefab);

                if (listItem.TryGetComponent<WeaponContainer>(out var weaponContainer))
                {
                    weaponContainer.equipment = weapon;

                    weaponContainer.SetupComponent();

                    if (gridContainer.TryGetComponent<RectTransform>(out var transform))
                    {
                        listItem.transform.SetParent(transform);
                        listItem.transform.localScale = Vector2.one;
                    }
                }

                containers.Add(listItem);
            }
        } else if (selectedTab.type == TabButton.Type.Armor)
        {
            foreach (Armor armor in armors)
            {
                GameObject listItem = (GameObject)PrefabUtility.InstantiatePrefab(weaponContainerPrefab);

                if (listItem.TryGetComponent<WeaponContainer>(out var weaponContainer))
                {
                    weaponContainer.equipment = armor;

                    weaponContainer.SetupComponent();

                    if (gridContainer.TryGetComponent<RectTransform>(out var transform))
                    {
                        listItem.transform.SetParent(transform);
                        listItem.transform.localScale = Vector2.one;
                    }
                }

                containers.Add(listItem);
            }
        }
        
    }

    void RemoveContainers ()
    {
        foreach (GameObject listItem in containers)
        {
            Destroy(listItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
