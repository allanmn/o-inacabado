using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Flags] public enum Type
    {
        None = 0,
        Weapon = 1,
        Armor = 2,
    }

    public TabGroup tabGroup;

    public Image background;

    public bool isDefaultSelected = false;

    public Type type = Type.Weapon | Type.Armor;

    public void OnPointerUp(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
