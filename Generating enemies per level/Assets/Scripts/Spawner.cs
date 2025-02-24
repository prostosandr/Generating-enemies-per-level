using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private EnemyPool _enemyPool;

    private List<SpawnPoint> _spawnPoints;

    private void Start()
    {
        _spawnPoints = new List<SpawnPoint>(FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None));

        StartCoroutine(SpawnTime());
    }

    private IEnumerator SpawnTime()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (enabled)
        {
            if (_enemyPool.NumberOEnemies < _enemyPool.PoolCapacity)
                Spawn();

            yield return wait;
        }
    }

    private void Spawn()
    {
        int minNuber = 0;

        SpawnPoint spawnPoint = _spawnPoints[Random.Range(minNuber, _spawnPoints.Count)];

        Enemy enemy = _enemyPool.GetEnemy();
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint.transform.position;
        enemy.StartLiveCycle(spawnPoint.GetRandomDirection());
    }
}