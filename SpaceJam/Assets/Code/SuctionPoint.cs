using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionPoint : MonoBehaviour
{
    // The object to be pulled into the point.
    [SerializeField]
    private Rigidbody2D bodyToPull;
    [SerializeField]
    private float influenceRadius;
    [SerializeField]
    private float strength;

    // Update is called once per frame
    void FixedUpdate()
    {
        // How far away is the player?
        // 0-1 = inside influence radius,
        // 1+ = outside influence radius
        float ratio = Vector2.Distance(bodyToPull.position, transform.position) / influenceRadius;

        if (ratio < 1)
        {
            float magnitude = (strength / ratio) * Time.fixedDeltaTime;
            bodyToPull.AddForce(magnitude * (new Vector2(transform.position.x, transform.position.y) - bodyToPull.position).normalized, ForceMode2D.Impulse);
        }

        if (ratio < 0.1)
        {
            PlayerProgress player = bodyToPull.GetComponent<PlayerProgress>();
            player.TakeDamage(99999);
        }
    }

    private void Update()
    {
        transform.eulerAngles += Vector3.forward * 500 * Time.deltaTime;
    }
}
