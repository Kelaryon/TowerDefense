using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnviromentalScript : ScriptableObject
{
    [SerializeField] List<GameObject> enviroment;

    public List<GameObject> ReturnEnviroList()
    {
        return enviroment;
    }
}
