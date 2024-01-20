using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Modal))]
public class UpgradeModal : MonoBehaviour
{
    public AnimationContainer animationContainer;

    public TextMeshProUGUI resultText;

    public Button button;

    public Color successColor;

    public Color failedColor;

    public Modal modal;

    private void Start()
    {
        modal = GetComponent<Modal>();
    }

    public void SetResult(bool success)
    {
        if (resultText != null)
        {
            resultText.text = success ? "SUCCESS!" : "FAILED!";
            resultText.color = success ? successColor : failedColor;
            resultText.gameObject.SetActive(true);
        }

        if (button != null)
        {
            button.gameObject.SetActive(true);
        }
    }
}
