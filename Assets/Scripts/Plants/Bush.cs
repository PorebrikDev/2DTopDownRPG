using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bush : MonoBehaviour, IDamageable
{
    private EnemyPool _enemyPool;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int damage = 1;
    private Transform _targer;
    [SerializeField] private PoolBrash _pooBrash;

    public event EventHandler OnTakeDamage;

    private void Awake()
    {
        _targer = transform;
        _enemyPool = FindAnyObjectByType<EnemyPool>();
    }
    public void Init(PoolBrash pool) { _pooBrash = pool; }
    public void TakeHit(Tool tool)
    {
        if (tool.Type == ToolType.Sword || tool.Type == ToolType.Axe)
        {
            if (currentHealth == maxHealth)
            {
                GameObject enemy = _enemyPool.GetEnemy(transform);

            }

            currentHealth -= damage;
            OnTakeDamage?.Invoke(this, EventArgs.Empty);
            if (currentHealth <= 0)
            {
                DestroyObject();
            }


        }
    }

    private void DestroyObject()
    {
        _pooBrash.ReturnToPool(gameObject);
        NavMeshSerfaceManager.Instance.RebakeNavPartOf();
    }

}
