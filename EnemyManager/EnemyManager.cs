using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public WaveManager wManager;
    public List<Enemy> enemyList;
    private void Awake()
    {
        enemyList = new List<Enemy>();
    }
    private void Start()
    {
        //GetAllCurrentEnemies();

    }
    public IEnumerator SpawnEnemy(List<EnemySpawn> eSpawnList, Bank bank)
    {
        foreach (EnemySpawn esp in eSpawnList)
        {
            yield return new WaitForSeconds(esp.timer);
            Enemy alfa = Instantiate(esp.enemy);
            alfa.InitializeEnemy(bank);
            alfa.gameObject.SetActive(true);

        }
    }
    public void AddEnemy(Enemy enemy)
    {
        enemyList.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }
    public List<Enemy> GetEnemyList()
    {
        return enemyList;
    }
    private void GetAllCurrentEnemies()
    {
        Enemy[] partList = gameObject.GetComponentsInChildren<Enemy>();
        foreach(Enemy en in partList)
        {
            AddEnemy(en);
        }
    }
    public void EnemyBankStart(Bank bank)
    {
        List<EnemySpawn> eSpawnList = wManager.oPool.PopulatePool(wManager.waveList);
        StartCoroutine(SpawnEnemy(eSpawnList, bank));
    }


}
