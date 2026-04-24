using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


public class EnemyManager : MonoBehaviour
{
    [SerializeField] MapReferenceScript mapRef;
    [SerializeField] List<EnemyWaveScriptableObject> waveList;
    [SerializeField] private bool targetingType;
    [SerializeField] private GameObject path;
    private Controller controller;
    private Dictionary<string, Enemy> enemyPrefabDictionary;
    private ObjectPool objPool;
    private Waypoint[] waypoints;
    public static Dictionary<Collider, Enemy> EnemyCacheByCollider = new();

    private void Awake()
    {
        objPool = new ObjectPool();
        waypoints = path.GetComponentsInChildren<Waypoint>();
    }

    private void Start()
    {
        controller = mapRef.GetController();
        controller.GameStart += ManagerSetup;
    }

    private IEnumerator SpawnEnemy()
    {
        objPool.AwakePopulatePool(controller, this, waypoints);
        foreach (EnemyWaveScriptableObject wave in waveList)
        {
            foreach (string enemyCode in wave.GetListOfEnemiesOrderedForSpawn())
            {
                if (enemyPrefabDictionary.TryGetValue(enemyCode, out var queuedEnemy))
                {
                    if (objPool.ActivateEnemy(queuedEnemy, targetingType))
                    {
                        Enemy enemy = Instantiate(queuedEnemy);
                        EnemyCacheByCollider.Add(enemy.GetComponent<Collider>(), enemy);
                        enemy.InitializeEnemy(controller, this, waypoints);
                        enemy.EnemyActivate(targetingType);
                    }

                    yield return new WaitForSeconds(wave.timeBetweenEnemies);
                }
                else
                {
                    Debug.Log("Index out of range check EnemyPrefabList and SO StringEnemyList");
                }
            }

            yield return new WaitForSeconds(wave.timeBetweenWaves);
        }
    }

    public void AddToPool(Enemy enemy)
    {
        objPool.AddToPool(enemy);
    }

    #region // Add and Remove ListMethod

    //List Methods
    public void AddEnemy(Enemy enemy)
    {
        controller.AddEnemyToList(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (targetingType)
        {
            controller.RemoveEnemyFromList(enemy);
        }
    }

    #endregion

    private void ManagerSetup()
    {
        enemyPrefabDictionary = mapRef.GetEnemyPrefabDictionary();
        objPool.SetPoolList(GetListOfEnemyTypesForCurrentWaves());
        StartCoroutine(SpawnEnemy());
    }

    private List<Enemy> GetListOfEnemyTypesForCurrentWaves()
    {
        List<Enemy> listOfEnemyTypes = new List<Enemy>();
        foreach (EnemyWaveScriptableObject wave in waveList)
        {
            listOfEnemyTypes.AddRange(
                wave.GetListOfEnemiesOrderedForSpawn()
                    .Select(enemyCode => enemyPrefabDictionary[enemyCode])
                    .Where(enemyPrefab => enemyPrefab is not null)
                    .ToList()
            );
        }
        return listOfEnemyTypes;
    }

    protected void AddEnemyToEnemyCacheByCollider(Enemy enemy)
    {
        EnemyCacheByCollider.Add(enemy.GetComponent<Collider>(), enemy);
    }
}

public class ObjectPool
{
    // The Dictionary holding the enemy Queue list
    private Dictionary<string, Queue<Enemy>> poolDictionary;

    //The Enemy Type List in to Spawn in the map;
    private List<Enemy> poolList;

    public void AwakePopulatePool(Controller bank, EnemyManager enemyManager, Waypoint[] waypoints)
    {
        poolDictionary = new Dictionary<string, Queue<Enemy>>();

        foreach (Enemy e in poolList)
        {
            Queue<Enemy> eQueue = new Queue<Enemy>();
            for (int i = 0; i < 3; i++)
            {
                Enemy enemy = Object.Instantiate(e);
                enemy.InitializeEnemy(bank, enemyManager, waypoints);
                enemy.gameObject.SetActive(false);
                eQueue.Enqueue(enemy);
            }

            poolDictionary.Add(e.GetEnemyType(), eQueue);
        }
    }

    public bool ActivateEnemy(Enemy enemy, bool targetType)
    {
        if (poolDictionary[enemy.GetEnemyType()].Count != 0)
        {
            Enemy enemyToSpawn = poolDictionary[enemy.GetEnemyType()].Dequeue();
            enemyToSpawn.EnemyActivate(targetType);
            return false;
        }
        else
        {
            return true;
        }
    }

    public void AddToPool(Enemy enemy)
    {
        poolDictionary[enemy.GetEnemyType()].Enqueue(enemy);
    }

    public void SetPoolList(List<Enemy> list)
    {
        poolList = list;
    }
}