using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[ExecuteInEditMode]
public class WaveManager : MonoBehaviour
{
    //public List<Enemy> testLista;
    public List<Wave> waveList;
    public ObjectPool oPool;

    //public void OnValidate()
    //{
    //    foreach (Wave wave in waveList)
    //    {
    //        //wave.enemyList.Clear();
    //        foreach (EnemyGroup en in wave.enemyGroups)
    //        {
    //            for (int k = 0; k < en.number; k++)
    //            {
    //                switch (en.eSelect)
    //                {
    //                    case EnemyGroup.EnemySelected.AL:
    //                        //wave.enemyList.Add(testLista[0]);
    //                        return;
    //                    case EnemyGroup.EnemySelected.Bal:
    //                        //wave.enemyList.Add(testLista[1]);
    //                        return;
    //                }
    //            }
    //        }
    //        Debug.Log("Ceva");
    //    }
    //}
    public void AddWave()
    {
        waveList.Add(new Wave());
    }
}
[System.Serializable]
public class Wave
{
    public List<EnemyGroup> enemyGroups;
    public float waveEndCooldown;
}
[System.Serializable]
public class EnemyGroup
{
    public int number;
    //public enum EnemySelected {AL,Bal};
    public Enemy eSelected;
    public float rate;
}

#if UNITY_EDITOR
[CustomEditor(typeof(WaveManager))]
[CanEditMultipleObjects]
public class WaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        WaveManager waveManager = (WaveManager)target;
        var alf = GUILayout.Button("TestButton");
        if (alf)
        {
            waveManager.AddWave();
        }
    }
}
#endif