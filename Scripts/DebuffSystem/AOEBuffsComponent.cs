using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AOEBuffsComponent : MonoBehaviour
{
    //[HideInInspector]
    [SerializeField] public float duration;
    [SerializeField] public bool isPermanent = true;
    [SerializeField] public bool pulse;
    [SerializeField] public ScriptableStatusEffect effect;
    [SerializeField] public float range;
    [SerializeField] public ScriptableEffect onHitEffect;
    private TargetLocatorPrime targetLocator;
    private HashSet<Enemy> enemyList;
    private HashSet<Tower> towerList;
    private UnityAction targetAction;
    //[HideInInspector]
    [SerializeField] public float pulseInterval = 0.3f;
    private float searchCooldown = 0.2f;
    private float cooldown;
    public enum TargetType
    {
        Enemy,
        Tower,
    }
    [SerializeField] public TargetType targetType;
    private void Start()
    {
        if (pulse)
        {
            searchCooldown = pulseInterval;
        }
        else
        {
            searchCooldown = 0.2f;
        }
        targetLocator = new TargetLocatorPrime();
        switch (targetType)
        {
            case TargetType.Enemy:
                enemyList = new HashSet<Enemy>();
                targetAction = GetEnemyList;
                break;
            case TargetType.Tower:
                towerList = new HashSet<Tower>();
                targetAction = GetTowerList;
                break;
        }
        if (!isPermanent)
        {
            DeleteComponent(duration);
        }
        
    }
    private void Update()
    {
        Tick();
    }
    private void GetEnemyList()
    {
        HashSet<Enemy> tempEnemyList = targetLocator.GetTargetsInRangePhysHash(this.transform.position, range);
        foreach (Enemy el in tempEnemyList)
        {
            if (enemyList.Contains(el))
            {
                enemyList.Remove(el);
            }
            else
            {
                //Add buff or defuff
            }
        }
        foreach(Enemy el in enemyList)
        {
            //Remove buff or debuff
        }
        enemyList = tempEnemyList;
    }
    private void GetTowerList()
    {
        HashSet<Tower> tempTowerList = targetLocator.GetTowersInRangePhysHash(this.transform.position, range);
        foreach(Tower to in tempTowerList)
        {
            if (towerList.Contains(to))
            {
                towerList.Remove(to);
            }
            else
            {
                //Add buff
            }
        }
        foreach(Tower to in towerList)
        {
            //Remove buff
        }
        towerList = tempTowerList;
    }
    //private void SetDuration()
    //{
    //    if (cooldown > 0)
    //    {
    //        cooldown -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        if (Pulse)
    //        {
    //            cooldown = PulseInterval;
    //        }
    //        else
    //        {
    //            cooldown = searchCooldown;
    //        }
    //        targetAction();
    //    }
    //    duration -= Time.deltaTime;
    //    if (duration < 0)
    //    {
    //        DeleteComponent();
    //    }
    //}
    private void Tick()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = searchCooldown;
            targetAction();
        }
    }
    public void DeleteComponent(float time)
    {
        Destroy(this.gameObject,time);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AOEBuffsComponent))]
public class RandomScript_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector(); // for other non-HideInInspector fields

        AOEBuffsComponent script = (AOEBuffsComponent)target;
        script.targetType = (AOEBuffsComponent.TargetType)EditorGUILayout.EnumPopup("TargetType", script.targetType);
        script.range = EditorGUILayout.FloatField("Range", script.range);
        script.effect = EditorGUILayout.ObjectField("Effect", script.effect, typeof(ScriptableStatusEffect), false) as ScriptableStatusEffect;
        script.isPermanent = EditorGUILayout.Toggle("IsPermanent", script.isPermanent);
        if (!script.isPermanent) // if bool is true, show other fields
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Duration", GUILayout.MaxWidth(190));
            script.duration = EditorGUILayout.FloatField(script.duration, GUILayout.MaxWidth(100));
            EditorGUILayout.EndHorizontal();
        }
        script.pulse = EditorGUILayout.Toggle("PulseType", script.pulse);
        if (script.pulse)
        {
            //EditorGUILayout.LabelField("PulseFrequency");
            script.onHitEffect = EditorGUILayout.ObjectField("OnHitEffect", script.onHitEffect, typeof(ScriptableEffect), false) as ScriptableEffect;
            script.pulseInterval = EditorGUILayout.FloatField("Pulse Interval", script.pulseInterval);
        }
    }
}
#endif
