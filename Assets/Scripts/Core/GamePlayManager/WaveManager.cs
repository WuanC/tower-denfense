using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.VFX;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        [SerializeField] private EnemyMovement enemyPrefabs;
        [SerializeField] private float enemySpeed;
        [SerializeField] private int enemyHealth;
        [SerializeField] private int poolSize = 10;
        [SerializeField] private int enemyInWave = 20;
        [SerializeField] private int coinsReceive;
        private ObjectPool<EnemyMovement> enemyPool;
        public int EnemyInWave {  get { return enemyInWave; } }
        public float EnemySpeed { get { return enemySpeed; } }
        public int EnemyHealth { get { return enemyHealth; } }
        public int CoinsReceive { get { return coinsReceive; } }

        public ObjectPool<EnemyMovement> GetEnemyPool()
        {
            return enemyPool;
        }
        public void InitEnemyPool(Transform enemyParent)
        {
            enemyPool = new ObjectPool<EnemyMovement>(enemyPrefabs, poolSize, enemyParent);
        }
    }
    private int currentEnemyCount;
    private bool isLastWave;
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private float timeBetweenEnemies;
    [SerializeField] private float timeBetweenWave = 5;

    private bool skipWave;
    private Coroutine waveCoroutine;

    private void Start()
    {
        EventManager.Instance.Register(EventID.OnGameStateChanged, OnGameStateChanged);
        EventManager.Instance.Register(EventID.OnSkipWave, SkipWave);
        EventManager.Instance.Register(EventID.OnEnemyDied, OnEnemyDied);
        foreach(Wave wave in waves)
        {
            wave.InitEnemyPool(enemyParent);
        }

    }
    IEnumerator StartWave()
    {
        for(int i = 0; i < waves.Length; i++)
        {
            EventManager.Instance.Broadcast(EventID.OnWaveStart,new WaveInfo(i + 1, waves[i].CoinsReceive, waves.Length));
            isLastWave = i == waves.Length - 1;
            currentEnemyCount += waves[i].EnemyInWave;
            for (int j = 0; j < waves[i].EnemyInWave; j++)
            {
                SpawnEnemy(waves[i].GetEnemyPool(), waves[i].EnemySpeed, waves[i].EnemyHealth, 30);
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            EventManager.Instance.Broadcast(EventID.OnWaveEnd, i != waves.Length - 1);
            yield return WaitForNextWave();

        }
    }
    private IEnumerator WaitForNextWave()
    {
        float timer = 0;
        skipWave = false;
        while (timer < timeBetweenWave)
        {
            if (skipWave) break;
            timer += Time.deltaTime;
            yield return null;
        }
    }
    private void SpawnEnemy(ObjectPool<EnemyMovement> enemyPool, float speed, int health, int coins)
    {
        EnemyMovement enemy = enemyPool.GetObject();
        enemy.Initialize(enemyPool, speed, health, coins);
    }
    public void SkipWave(object obj)
    {
        skipWave = true;
    }
    public void OnEnemyDied(object obj)
    {
        currentEnemyCount--;
        if(currentEnemyCount == 0 && isLastWave) {
            GameStateManager.Instance.SetState(GameState.Victory);
        }
    }
    private void OnGameStateChanged(object obj)
    {
        if(obj is GameState gameState)
        {
            switch(gameState)
            {
                case GameState.Selection:
                    EventManager.Instance.Broadcast(EventID.OnWaveStart, new WaveInfo(0, 0, waves.Length));
                    break;
                case GameState.Playing:
                    waveCoroutine = StartCoroutine(StartWave());
                    break;
                case GameState.Victory:
                case GameState.Defeat:
                    if (waveCoroutine != null)
                    {
                        StopCoroutine(waveCoroutine);
                        waveCoroutine = null;
                    }
                    break;
            }
        }
    }
    private void OnDisable()
    {
        EventManager.Instance?.Unregister(EventID.OnGameStateChanged, OnGameStateChanged);
        EventManager.Instance?.Unregister(EventID.OnSkipWave, SkipWave);
        EventManager.Instance?.Unregister(EventID.OnEnemyDied, OnEnemyDied);
    }
}
public class WaveInfo
{
    public WaveInfo(int currentWave, int bonusCoins, int waveLength)
    {
        this.currentWave = currentWave;
        this.bonusCoins = bonusCoins;
        this.waveLength = waveLength;
    }
    public int currentWave;
    public int bonusCoins;
    public int waveLength;
}

