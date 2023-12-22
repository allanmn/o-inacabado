using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public readonly CameraReadyEvent CameraReadyEvent = new();

    public GameObject initialObjectToGo;

    public Vector3 offset = new(0f, 3f, -10f);

    private bool isGoingToInitialObject = false;

    private bool isFollowingPlayer = false;

    public readonly float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

            if (Helpers.ArePositionsAlmostEqual(transform.position, targetPosition, 0.01f))
            {
                isGoingToInitialObject = false;
                CameraReadyEvent.Invoke();
                velocity = Vector3.zero;
                isFollowingPlayer = true;
            };
        }

        if (isFollowingPlayer == true) {
            Vector3 targetPosition = player.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, .2f);
        }
    }
}
