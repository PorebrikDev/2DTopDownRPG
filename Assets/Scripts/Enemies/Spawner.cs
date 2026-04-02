using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;

    private void Start()
    {
        GameObject enemy = enemyPool.GetEnemy(transform);
    }
}
