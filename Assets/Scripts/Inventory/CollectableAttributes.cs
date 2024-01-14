using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CollectableAttributes : MonoBehaviour
{
    public Image image;
    public TMP_Text quantity;
    public TMP_Text name;

    public void CollectableAttributesConstructor(Sprite sprite, string name)
    {
        this.image.sprite = sprite;
        this.name.text = name;
        this.quantity.text = "0";
    }
}
