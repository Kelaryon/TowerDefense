using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapReferenceScript : ScriptableObject
{
    private Controller controller;
    public Dictionary<string,Enemy> enemyPrefabDictionary;

    public void SetBank(Controller refController)
    {
        controller = refController;
    }
    public Controller GetController()
    {
        return controller;
    }

    public Dictionary<string,Enemy> GetEnemyPrefabDictionary()
    {
        return enemyPrefabDictionary;
    }
}
