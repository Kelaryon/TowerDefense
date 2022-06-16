using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour,IProjectileInterface
{
    Enemy target;
    [SerializeField] float bulletSpeed;
    public float damage;
    BoomerangTowerScript parentTower;
    byte bounces=0;
    //Used to instantiate the bullet from the tower 
    public void Setup(Enemy target, float damage, BoomerangTowerScript bTower)
    {
        this.damage = damage;
        parentTower = bTower;
        this.target = target;
    }

    //Old Code
    //private void Update()
    //{
    //    Attack(targetListPrime);
    //}
    //void TargetList(Transform firstTarget)
    //{
    //    List<Transform> targetList = new List<Transform>
    //    {
    //        firstTarget
    //    };
    //    targetList.Add(parentTower.tLocator.FindSecondClosestTarget(targetList[0].position));
    //    if (targetList[1] != null)
    //    {
    //        targetList.Add(parentTower.tLocator.FindSecondClosestTarget(targetList[1].position));
    //    }
    //    else
    //    {
    //        targetList.Add(parentTower.tLocator.FindSecondClosestTarget(targetList[0].position));
    //    }
    //    //Debug.Log("treia tinta =" + targetList[2].name);
    //    //foreach (Transform i in targetList)
    //    //{
    //    //    Attack(i, currentPos);
    //    //    Debug.Log("atac" + i);
    //    //}
    //    //Debug.Log("am Terminat nu nu am distrus:   " + targetList.Count);
    //    //Destroy(this.gameObject);
    //    int listSize = targetList.Count;
    //    for(int i = 1; i <= listSize; i++)
    //    {
    //        if(targetList[listSize-i] == null)
    //        {
    //            targetList.RemoveAt(listSize - i);
    //        }
    //    }
    //    //Debug.Log(targetList.Count + "Veridica Aicia");
    //    targetListPrime = targetList;
    //}
    // Used to find 3 targets for the bullet and create a list for it to travel
    //The attack sequence used in the update method for the movement of the bullet
    //void Attack(List<Transform> targets)
    //{
    //    if (targets.Count == 0)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        //Debug.Log(transform.position);
    //        transform.position = Vector3.MoveTowards(transform.position, targets[0].position + new Vector3(0,3.5f,0), bulletSpeed* Time.deltaTime);

    //        if (transform.position == targets[0].position + new Vector3(0,3.5f,0))
    //        {
    //            targets.RemoveAt(0);
    //            //Debug.Log(targets.Count);
    //        }

    //    }
    //}

    private void Update()
    {
        Attack();
    }
    void Attack()
    {
        try
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0, 3.5f, 0), 50 * Time.deltaTime);
            if (target.gameObject.activeSelf == false)
            {
                Destroy(this.gameObject);
            }
            if (this.transform.position.x == target.transform.position.x && this.transform.position.z == target.transform.position.z)
            {
                target.eHealth.IncDamage(damage);
                DamageModifier();
                target = parentTower.ReturnTargetLocator().FindClosestTargetInRange(target.transform.position, parentTower.ReturnManager().GetEnemyList(), 5f, 10f);
                if (target == null || bounces == 2)
                {
                    Destroy(this.gameObject);
                }
                bounces++;
                //Destroy(this.gameObject);
            }
        }
        catch (MissingReferenceException) {
            Destroy(this.gameObject);
        }
    }

    public void DamageModifier()
    {
        damage /= 2;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    target.eHealth.IncDamage(damage);
    //    DamageModifier();
    //}
}
