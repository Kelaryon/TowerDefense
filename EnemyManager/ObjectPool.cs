using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool;

    void Start()
    {
        //StartCoroutine(SpawnEnemy());
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        int i = 0;
        while (true && i < poolSize)
        {
            i++;
            //Debug.Log(i);
            //            EnableObjectInPool();
            GameObject newObj = Instantiate(enemyPrefab, transform);
            newObj.name = "Ghoul " + i;
            yield return new WaitForSeconds(spawnTimer);
        }
    }
    public List<EnemySpawn> PopulatePool(List<Wave> waveList)
    {
        List<EnemySpawn> eSpawn = new List<EnemySpawn>();
        foreach(Wave wave in waveList)
        {
            foreach (EnemyGroup eGroup in wave.enemyGroups)
            {
                for (int i = 0; i < eGroup.number; i++)
                {
                    eSpawn.Add(new EnemySpawn(eGroup.eSelected, eGroup.rate));
                }
            }
        }
        Shuffle<EnemySpawn>(eSpawn);
        return eSpawn;
    }
    void Shuffle<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }
}
public class EnemySpawn{
    public Enemy enemy;
    public float timer;

    public EnemySpawn(Enemy enemy, float timer)
    {
        this.enemy = enemy;
        this.timer = timer;
    }
}
