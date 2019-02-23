using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breach : MonoBehaviour
{
    // This is the button indicator for the generator.
    [SerializeField]
    private SpriteRenderer buttonIndicator;

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
            if (player.CrewQuartersCollectibles >= 3)
            {
                // Disable the trigger zone and start the process of removing darkness.
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                buttonIndicator.enabled = false;
                player.CrewQuartersComplete = true;
                player.CheckComplete();
            }
        }
    }
}
