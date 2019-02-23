using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    // This script basically handles everything that
    // isn't the physics for the player.


    [HideInInspector]
    public int EngineRoomCollectibles = 0;
    [HideInInspector]
    public int CrewQuartersCollectibles = 0;
    [HideInInspector]
    public int LifeSupportCollectibles = 0;
    [HideInInspector]
    public int GeneratorRoomCollectibles = 0;

    [SerializeField]
    private Slider oxygenSlider;
    [SerializeField]
    private int maxOxygenTime;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private float invulnerabilityTime;
    private float lastHitTime = 0;

    [SerializeField]
    private RawImage gameOverPanel;

    [SerializeField]
    private Rigidbody2D playerBody;

    [SerializeField]
    private float deathTransitionLength;
    private bool inDeath = false;
    private bool inRespawn = false;
    private float deathTransitionStartTime;


    public void Start()
    {
        oxygenSlider.maxValue = maxOxygenTime;
        oxygenSlider.value = maxOxygenTime;
        healthSlider.maxValue = 100;
        healthSlider.value = 100;
    }

    public void Update()
    {
        // This is a motherfuckin mess and im sorry

        // ...

        // but it works
        if (!inDeath)
        {
            oxygenSlider.value -= Time.deltaTime;
        }
        else
        {
            float ratio = (Time.time - deathTransitionStartTime) / deathTransitionLength;
            // Fade in
            if (inRespawn)
            {
                if (ratio < 1)
                {
                    gameOverPanel.color = new Color(0, 0, 0, 1 - ratio);
                }
                else
                {
                    Time.timeScale = 1;
                    inDeath = false;
                    inRespawn = false;
                    gameOverPanel.color = new Color(0, 0, 0, 0);
                }
            }
            // Fade out
            else
            {
                if (ratio < 1)
                {
                    gameOverPanel.color = new Color(0, 0, 0, ratio);
                }
                else
                {
                    playerBody.position = Vector2.zero;
                    oxygenSlider.value = oxygenSlider.maxValue;
                    healthSlider.value = healthSlider.maxValue;
                    deathTransitionStartTime = Time.time;
                    inRespawn = true;
                }
            }
        }
    }

    // Call this method from other objects to damage the player.
    public void TakeDamage(int damage)
    {
        // This makes sure the player has i-frames.
        if (Time.time - lastHitTime > invulnerabilityTime)
        {
            healthSlider.value -= damage;

            if (healthSlider.value <= 0)
            {
                // Game Over Sequence.
                Time.timeScale = 0.5f;
                inDeath = true;
                deathTransitionStartTime = Time.time;
            }

            lastHitTime = Time.time;
        }
    }
}
