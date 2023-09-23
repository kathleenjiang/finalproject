using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    // [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] easyEnemies;
    [SerializeField] private GameObject[] mediumEnemies;
    [SerializeField] private GameObject[] hardEnemies;
    [SerializeField] private GameObject[] bossEnemies;

    [Header("Attributes")]
    //number of enemies
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    //time between waves
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int currentWave = 1;
    //keep track of last spawned enemy
    private float timeSinceLastSpawn;
    //keep track of current amount of enemies
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    [Header("UI audio")]
    [SerializeField] GameObject victoryMenu;
    [SerializeField] AudioSource winSoundEffect;

    private enum GameState
    {
        Playing,
        Finished
    }

    private GameState gameState = GameState.Playing;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        victoryMenu.SetActive(false);
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        //if not spawning, nothing will run
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }

        if (gameState == GameState.Finished && enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            Victory();
        }
    }

    private void Victory()
    {
        victoryMenu.SetActive(true);
        winSoundEffect.Play();
        Time.timeScale = 0f;
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        if (currentWave == 20)
        {
            SpawnBoss();
        }

        if (currentWave > 20 && enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            gameState = GameState.Finished;
        }

        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        int index;
        GameObject prefabToSpawn;

        // Determine enemy type based on wave difficulty
        if (currentWave <= 5)
        {
            index = Random.Range(0, easyEnemies.Length);
            prefabToSpawn = easyEnemies[index];
        }
        else if (currentWave <= 10)
        {
            index = Random.Range(0, mediumEnemies.Length);
            prefabToSpawn = mediumEnemies[index];
        }
        else
        {
            index = Random.Range(0, hardEnemies.Length);
            prefabToSpawn = hardEnemies[index];
        }

        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private void SpawnBoss()
    {
        // Spawn boss prefab
        int index = Random.Range(0, bossEnemies.Length);
        GameObject bossPrefab = bossEnemies[index];
        Instantiate(bossPrefab, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    //progressively increase difficulty per wave
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
