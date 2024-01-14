using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private float currentStat, maxStat, targetStat;

    public void SetStatusBar(float currentValue, float maxValue)
    {
        currentStat = currentValue;
        targetStat = currentValue;
        maxStat = maxValue;
        slider.value = currentStat / maxStat;
    }

    public void UpdateStatusBar(float targetValue)
    {
        currentStat = targetStat;
        targetStat = targetValue;
    }


    // Update is called once per frame
    void Update()
    {
        if (currentStat > targetStat)
        {
            slider.value = Mathf.Lerp(currentStat / maxStat, targetStat / maxStat, 10f * Time.deltaTime);
            currentStat -= .05f;
        }

    }
}
