using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent (typeof(Image))]
public class UpgradeButton : MonoBehaviour
{
    public Button button;

    public Sprite idleSprite; 

    public Sprite disabledSprite;

    public ItemsController controller;

    public Image image;

    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    public void Upgrade()
    {
        SetEnabled(false);

        controller.TryToUpgrade();
    }

    public void SetEnabled(bool enabled)
    {
        button.enabled = enabled;

        button.image.sprite = enabled ? idleSprite : disabledSprite;
    }
}
