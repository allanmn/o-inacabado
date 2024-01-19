using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeAction
{
    FadeIn,
    FadeOut,
    FadeInAndOut,
    FadeOutAndIn
}

public class Fade : MonoBehaviour
{
    [Tooltip("The Fade Type.")]
    [SerializeField] public FadeAction fadeType;

    [Tooltip("the image you want to fade, assign in inspector")]
    [SerializeField] public Image img;

    public bool fade = false;

    public float maxAlpha = 1;

    public float fadeSpeed = 2;

    public void Start()
    {
        if (fadeType == FadeAction.FadeIn)
        {

            StartCoroutine(FadeIn());

        }

        else if (fadeType == FadeAction.FadeOut)
        {

            StartCoroutine(FadeOut());

        }

        else if (fadeType == FadeAction.FadeInAndOut)
        {

            StartCoroutine(FadeInAndOut());

        }

        else if (fadeType == FadeAction.FadeOutAndIn)
        {

            StartCoroutine(FadeOutAndIn());

        }
    }

    private void Update()
    {
        if (fade)
        {
            if (fadeType == FadeAction.FadeOut)
            {
                Color color = img.color;
                float i = color.a - (fadeSpeed * Time.deltaTime);
                color.a = i;
                img.color = color;

                if (color.a <= 0)
                {
                    fade = false;
                }
            }
        }
    }

    public void StartFade()
    {
        if (fadeType == FadeAction.FadeIn)
        {

            StartCoroutine(FadeIn());

        }

        else if (fadeType == FadeAction.FadeOut)
        {

            StartCoroutine(FadeOut());

        }

        else if (fadeType == FadeAction.FadeInAndOut)
        {

            StartCoroutine(FadeInAndOut());

        }

        else if (fadeType == FadeAction.FadeOutAndIn)
        {

            StartCoroutine(FadeOutAndIn());

        }
    }

    // fade from transparent to opaque
    IEnumerator FadeIn()
    {

        // loop over 1 second
        for (float i = 0; i <= maxAlpha; i += Time.deltaTime)
        {
            // set color with i as alpha
            Color color = img.color; color.a = i;
            img.color = color;
            yield return null;
        }

    }

    // fade from opaque to transparent
    IEnumerator FadeOut()
    {
        // loop over 1 second backwards
        for (float i = maxAlpha; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            Color color = img.color; color.a = i;
            img.color = color;
            yield return null;
        }
    }

    IEnumerator FadeInAndOut()
    {
        // loop over 1 second
        for (float i = 0; i <= maxAlpha; i += Time.deltaTime)
        {
            // set color with i as alpha
            Color color = img.color; color.a = i;
            img.color = color;
            yield return null;
        }

        //Temp to Fade Out
        yield return new WaitForSeconds(1);

        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            Color color = img.color; color.a = i;
            img.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOutAndIn()
    {
        // loop over 1 second backwards
        for (float i = maxAlpha; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            Color color = img.color; color.a = i;
            img.color = color;
            yield return null;
        }

        //Temp to Fade In
        yield return new WaitForSeconds(1);

        // loop over 1 second
        for (float i = 0; i <= maxAlpha; i += Time.deltaTime)
        {
            // set color with i as alpha
            Color color = img.color; color.a = i;
            img.color = color;
            yield return null;
        }
    }


}
