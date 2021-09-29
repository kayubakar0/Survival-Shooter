using UnityEngine;

public class PowerUpAttackSpeed : PowerUp
{
    public GameObject powerUpDestroyParticles;

    private PlayerStatus playerStatus;

    public float multiplier;
    public float powerUpDuration;

    private void Awake()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    private void Start()
    {
        playerStatus.attackSpeedPowerUpDuration = powerUpDuration;
    }

    public override void GetPowerUp()
    {
        playerStatus.attackSpeedMultiplier = multiplier;
        
        DestroyPowerUp();
    }

    public override void DestroyPowerUp()
    {
        Instantiate(powerUpDestroyParticles, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
