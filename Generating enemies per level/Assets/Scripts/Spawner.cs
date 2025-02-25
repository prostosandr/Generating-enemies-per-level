using System.Collections;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        StartCoroutine(Spawning());
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _spawnPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _spawnPoints[i] = transform.GetChild(i);
    }
#endif

    private IEnumerator Spawning()
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

        Transform spawnPoint = _spawnPoints[Random.Range(minNuber, _spawnPoints.Length)];

        Enemy enemy = _enemyPool.GetEnemy();
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint.transform.position;
        spawnPoint.TryGetComponent(out SpawnPoint currentSpawnPoint);
        enemy.StartLiveCycle(currentSpawnPoint.GetRandomDirection());
    }
}