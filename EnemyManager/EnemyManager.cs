using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<Enemy> enemyList;

    private void Start()
    {
        enemyList = new List<Enemy>();
        GetAllCurrentEnemies();
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
}
