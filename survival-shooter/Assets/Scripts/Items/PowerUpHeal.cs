using UnityEngine;

public class PowerUpHeal : PowerUp
{
    public GameObject powerUpDestroyParticles;

    private PlayerStatus playerStatus;

    public int healValue = 50;

    private void Awake()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    public override void GetPowerUp()
    {
        playerStatus.healValue = healValue;
        
        DestroyPowerUp();
    }

    public override void DestroyPowerUp()
    {
        Instantiate(powerUpDestroyParticles, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
