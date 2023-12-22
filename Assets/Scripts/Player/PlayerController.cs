using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Transform SpawnPoint;

    public bool waitForCameraBeReady = true;

    private GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (!waitForCameraBeReady)
        {
            Spawn();
        }
        else
        {
            whenCameraReadyListen();
        }
    }

    void whenCameraReadyListen()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController cameraController = MainCamera.GetComponent<CameraController>();
        cameraController.CameraReadyEvent.AddListener(whenCameraReady);
        Debug.Log("Adicionou Listener");
    }

    void whenCameraReady()
    {
        Spawn();
    }

    void Spawn()
    {
        if (SpawnPoint == null)
        {
            GameObject SpawnPoint = GameObject.FindGameObjectWithTag("Respawn");

            if (SpawnPoint != null)
            {
                transform.position = SpawnPoint.transform.position;
                SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
                renderer.sortingOrder = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
