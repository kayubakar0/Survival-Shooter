using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    public GameObject[] enemyPrefab;

    public GameObject FactoryMethod(int tag, Transform spawnpoint)
    {
        GameObject enemy = Instantiate(enemyPrefab[tag], spawnpoint.position, spawnpoint.rotation);

        return enemy;
    }
}
