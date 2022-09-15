using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class EnemyManager : MonoBehaviour
{
    [SerializeField] MapReferenceScript mapRef;
    private Bank bank;
    //private List<Enemy> enemyTargetList;
    private List<Enemy> enemyPrefabList;
    [SerializeField] List<EnemyWaveScriptableObject> waveList;
    private ObjectPool objPool;
    [SerializeField] private GameObject path;
    private Waypoint[] waypoints;
    private void Awake()
    {
        objPool = new ObjectPool();
        //enemyTargetList = new List<Enemy>();
        waypoints = path.GetComponentsInChildren<Waypoint>();

    }
    private void Start()
    {
        bank = mapRef.GetBank();
        bank.GameStart += ManagerSetup;
    }

    public IEnumerator SpawnEnemy()
    {
        objPool.AwakePopulatePool(bank, this, waypoints);
        foreach (EnemyWaveScriptableObject wave in waveList)
        {
            for (int i = 0; i < wave.enemyList.Count; i++)
            {
                if (wave.enemyList[i]<enemyPrefabList.Count)
                {
                    Enemy queuedEnemy = enemyPrefabList[wave.enemyList[i]];
                    if (objPool.ActivateEnemy(queuedEnemy))
                    {
                        Enemy enemy = Instantiate(queuedEnemy);
                        enemy.InitializeEnemy(bank, this, waypoints);
                        enemy.EnemyActivate();
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

    #region     // Add and Remove enemy from targetList()

    public void AddEnemy(Enemy enemy)
    {
        bank.AddEnemyToList(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        bank.RemoveEnemyFromList(enemy);
    }
     #endregion
    //private void GetAllCurrentEnemies()
    //{
    //    Enemy[] partList = gameObject.GetComponentsInChildren<Enemy>();
    //    foreach(Enemy en in partList)
    //    {
    //        AddEnemy(en);
    //    }
    //}
    public void ManagerSetup()
    {
        enemyPrefabList = mapRef.EnemyList;
        CheckLinks();
        objPool.SetPoolList(UniqueEnemyListGenerate());
        StartCoroutine(SpawnEnemy());
    }

    public List<Enemy> UniqueEnemyListGenerate()
    {
        //The array with indexes used to generate the enemies in the object pool
        int[] OPArray = null;
        List<Enemy> returnList = new List<Enemy>();
        foreach(EnemyWaveScriptableObject wave in waveList)
        {
            if (OPArray == null)
            {
                OPArray = wave.enemyList.ToArray();
            }
            else
            {
                OPArray = OPArray.Concat(wave.enemyList.ToArray()).ToArray();
            }
        }
        OPArray = OPArray.Distinct().ToArray();
        for(int i = 0; i < OPArray.Length; i++)
        {

            returnList.Add(enemyPrefabList[OPArray[i]]);

        }
        return returnList;
    }

    private void CheckLinks()
    {
        if(waveList[0].enemyName.Length > enemyPrefabList.Count)
        {
            Debug.LogException(new IndexOutOfRangeException("Enamy Wave List is Bigger than Enemy Prefab List an IndexOutOfRangeError can Occur.Check Wave SO Script and Map Reference SO to Resolve this ErrorMO"));
        }
        if(waveList[0].enemyName.Length < enemyPrefabList.Count)
        {
            Debug.LogWarning("Enemy Prefab List is Bigger than Wave Enemy List. Not all Enemys can be Spawned.Check Wave SO Script and Map Reference SO to Resolve this Warning");
        }
    }


}
public class ObjectPool
{
    // The Dictionary holding the enemy Queue list
    public Dictionary<string, Queue<Enemy>> poolDictionary;
    //The Enemy Type List in to Spawn in the map;
    private List<Enemy> poolList;

    public void AwakePopulatePool(Bank bank, EnemyManager enemyManager , Waypoint[] waypoints)
    {
        poolDictionary = new Dictionary<string, Queue<Enemy>>();

        foreach (Enemy e in poolList)
        {
            Queue<Enemy> eQueue = new Queue<Enemy>();
            for (int i = 0; i < 3; i++)
            {
                Enemy enemy = GameObject.Instantiate(e);
                enemy.InitializeEnemy(bank, enemyManager, waypoints);
                enemy.gameObject.SetActive(false);
                eQueue.Enqueue(enemy);
            }
            poolDictionary.Add(e.GetEnemyType(), eQueue);
            //Debug.Log(e.GetType());
        }
        //Utils.DisplayDictionary(poolDictionary);
    }

    //public Waypoint[] GetWaypoints(Waypoint[] waypoints)
    //{
    //    return waypoints;
    //}
    public bool ActivateEnemy(Enemy enemy)
    {
        if (poolDictionary[enemy.GetEnemyType()].Count != 0)
        {
            Enemy enemyToSpawn = poolDictionary[enemy.GetEnemyType()].Dequeue();
            enemyToSpawn.EnemyActivate();
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

    public void SetPoolList(List<Enemy> List)
    {
        poolList = List;
    }

    //Old Spawning method

    //public List<EnemySpawn> PopulatePool(List<Wave> waveList)
    //{
    //    List<EnemySpawn> eSpawn = new List<EnemySpawn>();
    //    foreach (Wave wave in waveList)
    //    {
    //        foreach (EnemyGroup eGroup in wave.enemyGroups)
    //        {
    //            for (int i = 0; i < eGroup.number; i++)
    //            {
    //                eSpawn.Add(new EnemySpawn(eGroup.eSelected, eGroup.rate));
    //            }
    //        }
    //    }
    //    Shuffle<EnemySpawn>(eSpawn);
    //    return eSpawn;
    //}
    //void Shuffle<T>(List<T> inputList)
    //{
    //    for (int i = 0; i < inputList.Count - 1; i++)
    //    {
    //        T temp = inputList[i];
    //        int rand = Random.Range(i, inputList.Count);
    //        inputList[i] = inputList[rand];
    //        inputList[rand] = temp;
    //    }
    //}
    //IEnumerator SpawnEnemy()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    int i = 0;
    //    while (true && i < poolSize)
    //    {
    //        i++;
    //        //Debug.Log(i);
    //        //            EnableObjectInPool();
    //        GameObject newObj = Instantiate(enemyPrefab, transform);
    //        newObj.name = "Ghoul " + i;
    //        yield return new WaitForSeconds(spawnTimer);
    //    }
    //}
}

//Enemy to Spawn and Timer to wait before the next one
//public class EnemySpawn
//{
//    public Enemy enemy;
//    public float timer;

//    public EnemySpawn(Enemy enemy, float timer)
//    {
//        this.enemy = enemy;
//        this.timer = timer;
//    }
//}
