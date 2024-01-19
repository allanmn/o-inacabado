using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeModal : MonoBehaviour
{
    // Start is called before the first frame update

    public AnimationContainer animationContainer;

    public TextMeshProUGUI resultText;

    public Color successColor;

    public Color failedColor;

    public void SetResult(bool success)
    {
        resultText.text = success ? "SUCCESS!" : "FAILED!";
        resultText.color = success ? successColor : failedColor;
    }
}
