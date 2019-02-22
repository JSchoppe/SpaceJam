using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;



    private bool isGrounded = false;
    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 forceToAdd = new Vector2();


        if (Input.GetAxis("Vertical") != 0)
        {
            forceToAdd += Input.GetAxis("Vertical") * Vector2.up * playerSpeed * Time.fixedDeltaTime;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            forceToAdd += Input.GetAxis("Horizontal") * Vector2.right * playerSpeed * Time.fixedDeltaTime;
        }


        playerRB.AddForce(forceToAdd, ForceMode2D.Impulse);
    }
}
