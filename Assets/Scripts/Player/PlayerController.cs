using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Transform SpawnPoint;

    public bool waitForCameraBeReady = true;

    public bool movePlayerAfterSpawn = true;

    private bool spawned = false;

    private GameObject MainCamera;

    public float cutsceneMoveSpeed = 2f;

    private float cutsceneMoveDuration = 1f;

    private Vector3 cutsceneStartPoint;
    private Vector3 cutsceneEndPoint;
    private float cutsceneStartTime = 0f;
    private int indexCutsceneWaypoint;
    private GameObject[] waypoints;




    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        Array.Reverse(waypoints);
        indexCutsceneWaypoint = 0;
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
                cutsceneStartPoint = transform.position;
                cutsceneStartTime = Time.time;
                if (waypoints.Length > 0)
                {
                    cutsceneEndPoint = waypoints[indexCutsceneWaypoint].transform.position;
                }
                spawned = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned && movePlayerAfterSpawn && waypoints.Length > 0)
        {
            movePlayerCutscene();
        }
    }

    void movePlayerCutscene()
    {
        var i = (Time.time - cutsceneStartTime) / cutsceneMoveDuration;
        transform.position = Vector3.Lerp(cutsceneStartPoint, cutsceneEndPoint, i);

        if (i >= 1)
        {
            cutsceneStartTime = Time.time;

            indexCutsceneWaypoint++;
            indexCutsceneWaypoint %= waypoints.Length;

            if (indexCutsceneWaypoint == 0) {
                movePlayerAfterSpawn = false;
            }

            cutsceneStartPoint = cutsceneEndPoint;
            cutsceneEndPoint = waypoints[indexCutsceneWaypoint].transform.position;
        }
    }

}
