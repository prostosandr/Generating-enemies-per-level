using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _numberOfEnemies;

    public int NumberOEnemies => _numberOfEnemies;
    public int PoolCapacity => _poolCapacity;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (enemy) => SubscribeToEnemy(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public Enemy GetEnemy()
    {
        _numberOfEnemies++;

        return _pool.Get();
    }

    private void SubscribeToEnemy(Enemy enemy)
    {
        enemy.Deactivated += ReleaseEnemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _numberOfEnemies--;

        enemy.Deactivated -= ReleaseEnemy;
        _pool.Release(enemy);
    }
}