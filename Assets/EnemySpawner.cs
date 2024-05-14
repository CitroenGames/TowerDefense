using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemy = 8;
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private float waveDelay = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.5f;

    [Header("Events")]
    public static UnityEvent OnEnemyKilled = new UnityEvent();


    private int currentWave = 0;
    private float timeSinceLastSpawn = 0f;
    private int AliveEnemies = 0;
    private int enemysNeedsSpawned = 0;
    private bool isSpawning = false;

    private void Awake()
    {
        OnEnemyKilled.AddListener(EnemyKilled);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;
        
        if(timeSinceLastSpawn >= (1f / spawnRate) && enemysNeedsSpawned > 0)
        {
            SpawnEnemy();
            // less enemies need to be spawned
            enemysNeedsSpawned--;
            AliveEnemies++;
            timeSinceLastSpawn = 0f;
        }

        if (AliveEnemies == 0 && enemysNeedsSpawned == 0 && isSpawning)
        {
            EndWave();
        }
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(waveDelay);
        enemysNeedsSpawned = EnemiesPerWave();
        isSpawning = true;
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        GameObject prefabtospawn = enemyPrefabs[0];
        Instantiate(prefabtospawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private void EnemyKilled()
    {
        AliveEnemies--;
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemy * Mathf.Pow(currentWave, 0.8f));
    }
}
