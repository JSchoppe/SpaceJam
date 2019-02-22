using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform playerTarget;

    private Camera cam;

    // This array represents the different camera regions for the game.
    // The camera will lock into a zone until the player moves to another screen.
    // These must be the same length!
    [SerializeField]
    private Vector2[] cameraMins;
    [SerializeField]
    private Vector2[] cameraMaxes;

    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();

        offset = cam.transform.position - playerTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current camera region of the map.
        Vector2[] zone = GetCurrentZone();

        // Set the camera position to the player (plus its initial offset).
        cam.transform.position = playerTarget.position + offset;

        // -10 is required in the following functions, otherwise the camera moves to
        // z = 0 (and thus doesn't render some things).

        // Check if the camera is too far to the left.
        if (cam.transform.position.x - cam.orthographicSize * cam.aspect < zone[0].x)
        {
            // Move the camera back to the right.
            cam.transform.position = new Vector3
            (
                zone[0].x + cam.orthographicSize * cam.aspect,
                cam.transform.position.y,
                -10
            );
        }
        // Check if the camera is too far to the right
        else if (cam.transform.position.x + cam.orthographicSize * cam.aspect > zone[1].x)
        {
            // Move the camera back to the left.
            cam.transform.position = new Vector3
            (
                zone[1].x - cam.orthographicSize * cam.aspect,
                cam.transform.position.y,
                -10
            );
        }

        // Check if the camera is too low.
        if (cam.transform.position.y - cam.orthographicSize < zone[0].y)
        {
            // Move the camera back up.
            cam.transform.position = new Vector3
            (
                cam.transform.position.x,
                zone[0].y + cam.orthographicSize,
                -10
            );
        }
        // Check if the camera is too high.
        else if (cam.transform.position.y + cam.orthographicSize > zone[1].y)
        {
            // Move the camera back down.
            cam.transform.position = new Vector3
            (
                cam.transform.position.x,
                zone[1].y - cam.orthographicSize,
                -10
            );
        }
    }

    // This code figures out which region of the map the player is currently in.
    private Vector2[] GetCurrentZone()
    {
        // Cycle through all camera zones.
        for (int i = 0; i < cameraMins.Length; i++)
        {
            if (playerTarget.position.x > cameraMins[i].x && playerTarget.position.x < cameraMaxes[i].x)
            {
                if (playerTarget.position.y > cameraMins[i].y && playerTarget.position.y < cameraMaxes[i].y)
                {
                    return new Vector2[] { cameraMins[i], cameraMaxes[i]};
                }
            }
        }

        Debug.Log("Player escaped camera region!");
        return new Vector2[] { new Vector2(-10000, -10000), new Vector2(10000,10000)};
    }

}
