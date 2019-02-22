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
        // Get the current camera region.
        //Vector2[] zone = GetCurrentZone();


        cam.transform.position = playerTarget.position + offset;
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
        return new Vector2[] { new Vector2(-1000, -1000), new Vector2(1000,1000)};
    }

}
