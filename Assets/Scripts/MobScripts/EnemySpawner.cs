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
    [SerializeField] private GameObject[] round8;
    [SerializeField] private GameObject[] round9;
    [SerializeField] private GameObject[] round10;
    [SerializeField] private GameObject[] round11;
    [SerializeField] private GameObject[] round12;
    [SerializeField] private GameObject[] round13;
    [SerializeField] private GameObject[] round14;
    [SerializeField] private GameObject[] round15;
    [SerializeField] private GameObject[] round16;
    [SerializeField] private GameObject[] round17;
    [SerializeField] private GameObject[] round18;
    [SerializeField] private GameObject[] round19;
    [SerializeField] private GameObject[] round20;
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
    private GameObject[][] enemyPrefab = new GameObject[20][];

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
        enemyPrefab[7] = round8;
        enemyPrefab[8] = round9;
        enemyPrefab[9] = round10;
        enemyPrefab[10] = round11;
        enemyPrefab[11] = round12;
        enemyPrefab[12] = round13;
        enemyPrefab[13] = round14;
        enemyPrefab[14] = round15;
        enemyPrefab[15] = round16;
        enemyPrefab[16] = round17;
        enemyPrefab[17] = round18;
        enemyPrefab[18] = round19;
        enemyPrefab[19] = round20;
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