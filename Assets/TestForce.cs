using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("JUMPED=");
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
