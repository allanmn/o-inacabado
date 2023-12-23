using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SlashController : MonoBehaviour
{
    private BoxCollider2D slashCollider;
    private TrailRenderer slashTrail;
    private bool isSlashing;

    public Vector2 slashDirection { get; private set; }
    public float minSlashVelocity = 0.001f;

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    // Start is called before the first frame update
    void Awake()
    {
        slashCollider = GetComponent<BoxCollider2D>();
        slashTrail = GetComponentInChildren<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        transform.position = newPosition;

        isSlashing = true;
        slashCollider.enabled = true;
        slashTrail.enabled = true;
        slashTrail.Clear();
    }

    private void StopSlicing()
    {
        slashCollider.enabled = false;
        isSlashing = false;
        slashTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        slashDirection = newPosition - transform.position;

        float velocity = slashDirection.magnitude / Time.deltaTime;

        slashCollider.enabled = velocity > minSlashVelocity;

        transform.position = newPosition;
    }
}