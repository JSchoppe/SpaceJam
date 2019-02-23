using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private RawImage darknessOverlay;

    // This is the button indicator for the generator.
    [SerializeField]
    private SpriteRenderer buttonIndicator;

    private bool powerTurningOn = false;

    [SerializeField]
    private float powerOnLength;
    private float powerOnStartTime;

    private void Start()
    {
        buttonIndicator.enabled = false;
    }

    // Turn the button indicator on and off when player enters trigger.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        buttonIndicator.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        buttonIndicator.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Assumed that the trigger enter is from the player.

        // Is the player pressing the A button?
        if (Input.GetButtonDown("Fire1"))
        {
            // Get the player that is in the trigger.
            PlayerProgress player = collision.gameObject.GetComponent<PlayerProgress>();

            // Does the player have the neccasary collectibles?
            if (player.GeneratorRoomCollectibles >= 3)
            {
                // Disable the trigger zone and start the process of removing darkness.
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                buttonIndicator.enabled = false;

                // Start the animation of turning the power on.
                powerTurningOn = true;
                powerOnStartTime = Time.time;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Animation for the power turning back on.
        if (powerTurningOn)
        {
            float ratio = (Time.time - powerOnStartTime) / powerOnLength;
            if (ratio < 1)
            {
                darknessOverlay.color = new Color(1, 1, 1, 1 - ratio);
            }
            else
            {
                // End the animation.
                darknessOverlay.enabled = false;
                powerTurningOn = false;
            }
        }
    }
}
