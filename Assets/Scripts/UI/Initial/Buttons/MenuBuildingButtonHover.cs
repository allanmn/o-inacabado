using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuBuildingButtonHover : MonoBehaviour
{
    Vector3 originalScale;

    public float scaleFactor = 1.2f;

    public float timeToScale = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter()
    {
        LeanTween.scale(gameObject, originalScale * scaleFactor, timeToScale);
    }

    public void OnPointerExit()
    {
        LeanTween.scale(gameObject, originalScale, timeToScale);
    }
}
