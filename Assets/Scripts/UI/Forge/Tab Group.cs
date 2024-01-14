using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> buttons;

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    public TabButton selectedTab;

    public ForgeItemsController itemsController;

    public void Subscribe(TabButton button)
    {
        if (buttons == null)
        {
            buttons = new List<TabButton> ();
        }

        buttons.Add(button);

        if (button.isDefaultSelected)
        {
            OnTabSelected(button);
        }
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        NotifyItemsController(button);
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
    }

    public void NotifyItemsController(TabButton button)
    {
        if (itemsController != null)
        {
            itemsController.TabChanged(button);
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in buttons)
        {
            if (selectedTab != null && selectedTab == button)
            {
                continue;
            }
            button.background.sprite = tabIdle;
        }
    }
}
