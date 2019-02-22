using UnityEngine;
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

    public void Start()
    {
        oxygenSlider.maxValue = maxOxygenTime;
        oxygenSlider.value = maxOxygenTime;
        healthSlider.maxValue = 100;
        healthSlider.value = 100;
    }

    public void Update()
    {
        oxygenSlider.value -= Time.deltaTime;
    }

    // Call this method from other objects to damage the player.
    public void TakeDamage(int damage)
    {
        // This makes sure the player has i-frames.
        if (Time.time - lastHitTime < invulnerabilityTime)
        {
            healthSlider.value -= damage;
            lastHitTime = Time.time;
        }
    }
}
