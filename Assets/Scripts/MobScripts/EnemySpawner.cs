using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] round1;
    [SerializeField] private GameObject[] round2;
    [SerializeField] private GameObject[] round3;
    [SerializeField] private GameObject[] round4;
    [SerializeField] private GameObject[] round5;
    [SerializeField] private GameObject[] round6;
    [SerializeField] private GameObject[] round7;
    [SerializeField] private LevelManager levelManager;
    public static EnemySpawner main;

    [Header("Events")]
    public static UnityEvent OnEnemyDestroy = new UnityEvent();
    
    [Header("Attributes")]
    [SerializeField] private float enemiesPerSecond = 0.5f;

    public int currentWave = 0;
    private float timeSinceLastSpawn;
    private int enemiesAlive = 0;
    private int enemiesSpawned = 0;
    private int enemiesLeftToSpawn;
    public bool isSpawning = false;
    private GameObject[][] enemyPrefab = new GameObject[7][];

    private void Awake()
    {
	    main = this;
        OnEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        enemyPrefab[0] = round1;
        enemyPrefab[1] = round2;
        enemyPrefab[2] = round3;
        enemyPrefab[3] = round4;
        enemyPrefab[4] = round5;
        enemyPrefab[5] = round6;
        enemyPrefab[6] = round7;
    }

    private void Update()
    {
        if (!isSpawning) return;
        
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            enemiesSpawned++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    
    public void StartWave()
    {
	    isSpawning = true;
	    enemiesSpawned = 0;
	    enemiesLeftToSpawn = enemyPrefab[currentWave].Length;
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        levelManager.gold += 100*(currentWave+1);
        currentWave++;
    }

    private void SpawnEnemy()
    {
        GameObject[] prefabsToSpawn = enemyPrefab[currentWave];
	    Instantiate(prefabsToSpawn[enemiesSpawned], LevelManager.main.startPoint.position, Quaternion.identity);
    }
}