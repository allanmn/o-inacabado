using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public readonly CameraReadyEvent CameraReadyEvent = new();

    public GameObject initialObjectToGo;

    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private bool isGoingToInitialObject = false;

    private readonly float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (initialObjectToGo != null)
        {
            isGoingToInitialObject = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingToInitialObject == true)
        {
            Vector3 targetPosition = initialObjectToGo.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            Debug.Log("Me");
            Debug.Log("X: " + transform.position.x + " Y: " + transform.position.y + " Z: " + transform.position.z);
            Debug.Log("Target");
            Debug.Log("X: " + targetPosition.x + " Y: " + targetPosition.y + " Z: " + targetPosition.z);

            if (Helpers.ArePositionsAlmostEqual(transform.position, targetPosition, 0.01f))
            {
                isGoingToInitialObject = false;
                CameraReadyEvent.Invoke();
            };
        }
    }
}
