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

    [Header("Events")]
    public static UnityEvent OnEnemyDestroy = new UnityEvent();
    
    [Header("Attributes")]
    [SerializeField] private float enemiesPerSecond = 0.5f;

    private int currentWave = 0;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    public bool isSpawning = false;

    private void Awake()
    {
        OnEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        Gameobject[][] enemyPrefab = {round1, round2, round3, round4, round5, round6, round7, round8, round9, round10,
         round11, round12, round12, round13, round14, round15, round16, round17, round18, round19, round20};
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
        for (int i = 0; i < prefabsToSpawn.Length; i++)
        {
            Instantiate(prefabsToSpawn[i], LevelManager.main.startPoint.position, Quaternion.identity);
        }  
    }
}