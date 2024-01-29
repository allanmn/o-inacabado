using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerController : MonoBehaviour
{
    UIController uiController;

    // Start is called before the first frame update
    void Awake()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            uiController.DecreaseShieldHits(collision.GetComponent<EnemyAttributes>().damage);
        }

        Destroy(collision.gameObject);
    }
}
