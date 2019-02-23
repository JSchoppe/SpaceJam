using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluctuatingHazard : MonoBehaviour
{

    [SerializeField]
    int hitDamage;
    [SerializeField]
    int knockback;

    [SerializeField]
    float fluctuationChance;

    [SerializeField]
    float fluctuationMinimumRange;

    [SerializeField]
    Vector2 fluctuationRange;

    [SerializeField]
    GameObject visualReference;

    [SerializeField]
    Light lightReference;

    //
    CircleCollider2D hitArea;

    //
    private float currentHitArea;

    // Start is called before the first frame update
    void Start()
    {

        //
        hitArea = this.gameObject.GetComponent<CircleCollider2D>();

        //
        currentHitArea = Random.Range(fluctuationRange.x, fluctuationRange.y + fluctuationMinimumRange);

    }

    // Update is called once per frame
    void Update()
    {

        // Randomized chance of radius expanding
        // Cast to int to prevent floating values
        if ((int)Random.Range(0, fluctuationChance + 2) == (int)fluctuationMinimumRange)
        {

            // Randomize hit area radius
            currentHitArea = Random.Range(fluctuationRange.x, fluctuationRange.y + fluctuationMinimumRange);

            // Prevent hit area from going beyond max range
            if (currentHitArea > fluctuationRange.y) currentHitArea = fluctuationRange.y;

        }
        else
        {
       
            // Expand radius
            hitArea.radius = Mathf.Lerp(hitArea.radius, currentHitArea, 0.1f);

            // Prepare current scale and final scale
            Vector3 currentScale = visualReference.transform.localScale;
            Vector3 finalScale = new Vector3(currentHitArea * 2, currentHitArea * 2, 1);

            // Lerp to Scale
            visualReference.transform.localScale = Vector3.Lerp(currentScale, finalScale, 0.1f);

            // Fancy Light Effects
            lightReference.intensity = Mathf.Lerp(lightReference.intensity, Random.Range(1, 5), 0.1f);

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
