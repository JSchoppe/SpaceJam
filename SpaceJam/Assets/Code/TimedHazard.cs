using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedHazard : MonoBehaviour
{
    [SerializeField]
    private int hitDamage;
    [SerializeField]
    private int knockback;

    [SerializeField]
    private bool turnsOnAndOff;
    [SerializeField]
    private float onTime;
    [SerializeField]
    private float offTime;

    [SerializeField]
    private float timeOffset;
    private bool offsetWait = true;

    private bool isOn = true;
    private float lastFlipTime = 0;


    private BoxCollider2D hitbox;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = gameObject.GetComponent<BoxCollider2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (offsetWait)
        {
            // This block is probably redundant.
            if (Time.time > timeOffset)
            {
                offsetWait = false;
                lastFlipTime = Time.time;
            }
        }
        else
        {
            if (turnsOnAndOff)
            {
                if (isOn)
                {
                    if (Time.time - lastFlipTime > onTime)
                    {
                        isOn = false;
                        hitbox.enabled = false;
                        lastFlipTime = Time.time;

                        renderer.color = new Color(1, 1, 1, 0);
                    }
                }
                else
                {
                    if (Time.time - lastFlipTime > offTime)
                    {
                        isOn = true;
                        hitbox.enabled = true;
                        lastFlipTime = Time.time;

                        renderer.color = new Color(1, 1, 1, 1);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerProgress player = collision.gameObject.GetComponent<PlayerProgress>();
        Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
        player.TakeDamage(hitDamage);

        playerBody.velocity = (playerBody.transform.position - transform.position) * knockback;
    }
}
