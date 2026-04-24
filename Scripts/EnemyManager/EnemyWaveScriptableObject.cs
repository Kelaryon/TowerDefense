using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
#endif


[CreateAssetMenu(menuName = "EnemyManagement/Waves")]
[Serializable]
public class EnemyWaveScriptableObject : ScriptableObject
{
    [HideInInspector]
    private enum AvailableEnemies
    {
        EnemyA,
        EnemyB
    }
    private List<AvailableEnemies> listOfEnemiesOrderedForSpawn;
    public float timeBetweenEnemies;
    public float timeBetweenWaves;

    public List<string> GetListOfEnemiesOrderedForSpawn()
    {
        return listOfEnemiesOrderedForSpawn
            .Select(e => e.ToString())
            .ToList();
    }

    public void DebuggUI()
    {
        for (int i = 0; i < listOfEnemiesOrderedForSpawn.Count; i++)
        {
            Debug.Log("Position " + i + ":" + listOfEnemiesOrderedForSpawn[i]);
        }
    }
}