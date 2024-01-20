using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class AnimationContainer : MonoBehaviour
{
    public GameObject hammer;

    public GameObject anvil;

    public void ShowAnimation()
    {
        if (anvil != null)
        {
            if (anvil.TryGetComponent<Fade>(out var component))
            {
                component.fadeType = FadeAction.FadeIn;
                component.StartFade();
            }
        }

        if (hammer != null)
        {
            if (hammer.TryGetComponent<Animator>(out var animator)) {
                animator.enabled = true;
            }

            if (hammer.TryGetComponent<Fade>(out var component))
            {
                component.fadeType = FadeAction.FadeIn;
                component.StartFade();
            }
        }
    }

    public void HideAnimation()
    {
        if (anvil != null)
        {
            if (anvil.TryGetComponent<Fade>(out var component))
            {
                component.fadeType = FadeAction.FadeOut;
                component.fade = true;
            }
        }

        if (hammer != null)
        {
            if (hammer.TryGetComponent<Fade>(out var component))
            {
                component.fadeType = FadeAction.FadeOut;
                component.fade = true;
            }
        }
    }
}
