using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapReferenceScript : ScriptableObject
{
    Bank bank;
    //EnemyManager enemyManager;
    public List<Enemy> EnemyList;

    //public EnemyManager EnemyManager { get => enemyManager; set => enemyManager = value; }

    public void SetBank(Bank refBank)
    {
        bank = refBank;
    }
    public Bank GetBank()
    {
        return bank;
    }

    public List<Enemy> ReturnList()
    {
        return EnemyList;
    }
}
