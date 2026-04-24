using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetLocatorPrime
{
    public Enemy FindClosestTarget(Vector3 position, List<Enemy> enemyList) {
        Enemy closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemyList)
        {
            if(enemy == null)
            {
                continue;
            }
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
    public Enemy FindClosestTargetInRange(Vector3 position, List<Enemy> enemyList,float rangeMin, float rangeMax)
    {
        Enemy closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemyList)
        {
            float targetDistance = Vector3.Distance(position, enemy.transform.position);
            if (targetDistance < maxDistance && targetDistance > rangeMin && targetDistance < rangeMax) {
                closestTarget = enemy;
                //Debug.Log(enemy + " :" + targetDistance + " ClosestEnemy:" + closestTarget);
                maxDistance = targetDistance; 
            }
        }
        return (closestTarget);
    }
    public Enemy FindClosestTargetInRange(Vector3 position, List<Enemy> enemyList,float range)
    {
        Enemy closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemyList)
        {
            float targetDistance = Vector3.Distance(position, enemy.transform.position);
            if (targetDistance < maxDistance && targetDistance < range)
            {
                closestTarget = enemy;
                maxDistance = targetDistance;
            }
        }
        return (closestTarget);
    }
    public List<Enemy> GetTargetInRange(Vector3 position, List<Enemy> enemyList, float range)
    {
        List<Enemy> enemyListInRange = new List<Enemy>();
        foreach (Enemy enemy in enemyList)
        {
            float targetDistance = Vector3.Distance(position, enemy.transform.position);
            if (targetDistance < range)
            {
                enemyListInRange.Add(enemy);
            }
        }
        return (enemyListInRange);
    }

    public List<Enemy> GetTargetsInRangePhys(Vector3 position, float range)
    {
        List<Enemy> enemyList = new List<Enemy>();
        Collider[] enemyColliders = Physics.OverlapSphere(position,range,1 << 6);
        foreach(Collider col in enemyColliders)
        {
            enemyList.Add(col.GetComponent<Enemy>());
        }
        return enemyList;
    }
    public HashSet<Enemy> GetTargetsInRangePhysHash(Vector3 position, float range)
    {
        HashSet<Enemy> enemyList = new HashSet<Enemy>();
        Collider[] enemyColliders = Physics.OverlapSphere(position, range, 1 << 6);
        foreach (Collider col in enemyColliders)
        {
            enemyList.Add(col.GetComponent<Enemy>());
        }
        return enemyList;
    }
    public HashSet<Tower> GetTowersInRangePhysHash(Vector3 position, float range)
    {
        HashSet<Tower> towerList = new HashSet<Tower>();
        Collider[] enemyColliders = Physics.OverlapSphere(position, range, 1 << 7);
        foreach (Collider col in enemyColliders)
        {
            towerList.Add(col.GetComponent<Tower>());
        }
        return towerList;
    }
    public Enemy GetTargetInRangePhys(Vector3 position, float range)
    {
        Collider closestEnemy = Physics.OverlapSphere(position, range, 1 << 6)
            .OrderBy(e => Vector3.Distance(position, e.transform.position))
            .FirstOrDefault(null);
        return closestEnemy?.GetComponent<Enemy>();
        
    }
    public Enemy GetTargetInRangeFarthestPhys(Vector3 position, float range)
    {
        return GetTargetsInRangePhys(position, range)
            .OrderByDescending(e => e.GetTravelDistance())
            .FirstOrDefault(null);
    }
}
