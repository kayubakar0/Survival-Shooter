using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public float spawnTime = 3f;
    public GameObject[] powerUpPfb;
    public Transform[] spawnPoints;

    private void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Spawn()
    {
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int spawnPowerUp = Random.Range(0, powerUpPfb.Length);

        if (spawnPoints[spawnPointIndex].childCount > 0)    // Prevent spawn power-up in the same spawnpoint
            return;

        Instantiate(powerUpPfb[spawnPowerUp], spawnPoints[spawnPointIndex]);
    }
}
