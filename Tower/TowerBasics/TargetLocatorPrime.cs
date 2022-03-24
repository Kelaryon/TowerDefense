using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocatorPrime : MonoBehaviour
{
    static public Transform FindClosestTarget(Vector3 position)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        return(closestTarget);
    }
    static public Enemy FindClosestTarget(Vector3 position, List<Enemy> enemyList) {
        Enemy closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemyList)
        {
            float targetDistance = Vector3.Distance(position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy;
                maxDistance = targetDistance;
            }
        }
        return (closestTarget);
    }
    //Work in Progress
    static public Enemy FindClosestTargetInRange(Vector3 position, List<Enemy> enemyList,float range)
    {
        Enemy closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemyList)
        {
            float targetDistance = Vector3.Distance(position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy;
                maxDistance = targetDistance;
            }
        }
        return (closestTarget);
    }
    static public Transform FindSecondClosestTarget(Vector3 position)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(position, enemy.transform.position);
            if (targetDistance < maxDistance && targetDistance > 0f)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        return (closestTarget);
    }
    public void AimWeapon(Transform target)
    {
        if (target != null)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
        }
    }
}
