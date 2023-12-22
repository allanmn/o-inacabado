using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    [SerializeField]
    float maxHealth, currentHealth, currentMoveSpeed;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    int experience, gold;
    [SerializeField]
    public int level, identifier;
    [SerializeField]
    new string name;
    // Start is called before the first frame update

    void Awake()
    {
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -1 * moveSpeed * Time.deltaTime, 0);
    }
}
