using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SliderButton : MonoBehaviour
{
    public Sprite disabledImage;
    public Sprite idleImage;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateImage(bool disabled)
    {
        if (image == null) {
            image = GetComponent<Image>();
        }

        if (disabled)
        {
            image.sprite = disabledImage;
        } else
        {
            image.sprite = idleImage;
        }
    }

    public void SetDisabled(bool disabled)
    {
        UpdateImage(disabled);
        if (TryGetComponent<Button>(out var button))
        {
            button.interactable = !disabled;
        }
    }
}
