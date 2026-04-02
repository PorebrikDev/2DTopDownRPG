using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    [SerializeField] private int _poolSize = 2;
    private List<GameObject> _pool = new List<GameObject>();
    [SerializeField] private Transform poolRoot;
    [SerializeField] private Transform lokalSlime;

    private void Awake()
    {

        for (int i = 0; i < _poolSize; i++)
        {
            var obj = Instantiate(prefab, poolRoot);
            obj.SetActive(false);
            obj.GetComponent<EnemyEntity>().Init(this);
            _pool.Add(obj);
        }
    }
    public GameObject GetEnemy(Transform spawnPosition)
    {
        foreach (var enemy in _pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.SetParent(lokalSlime);
                enemy.transform.position = spawnPosition.position;
                enemy.SetActive(true);
                return enemy;
            }
        }
        var obj = Instantiate(prefab, lokalSlime);
        obj.transform.SetParent(lokalSlime);
        obj.transform.position = spawnPosition.position;
        obj.GetComponent<EnemyEntity>().Init(this);
        obj.SetActive(true);
        _pool.Add(obj);
        return obj;
    }
    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.SetParent(poolRoot);
    }



}
