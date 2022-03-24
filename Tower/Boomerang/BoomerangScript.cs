using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour,IProjectileInterface
{
    List<Transform> targetListPrime;
    [SerializeField] float bulletSpeed;
    public float damage;
    //Used to instantiate the bullet from the tower 
    public void Setup(Transform firstTarget, float damage)
    {
        TargetList(firstTarget);
        this.damage = damage;
    }
    private void Update()
    {
        Attack(targetListPrime);
    }
    // Used to find 3 targets for the bullet and create a list for it to travel
    void TargetList(Transform firstTarget)
    {
        List<Transform> targetList = new List<Transform>
        {
            firstTarget
        };
        targetList.Add(TargetLocatorPrime.FindSecondClosestTarget(targetList[0].position));
        if (targetList[1] != null)
        {
            targetList.Add(TargetLocatorPrime.FindSecondClosestTarget(targetList[1].position));
        }
        else
        {
            targetList.Add(TargetLocatorPrime.FindSecondClosestTarget(targetList[0].position));
        }
        //Debug.Log("treia tinta =" + targetList[2].name);
        //foreach (Transform i in targetList)
        //{
        //    Attack(i, currentPos);
        //    Debug.Log("atac" + i);
        //}
        //Debug.Log("am Terminat nu nu am distrus:   " + targetList.Count);
        //Destroy(this.gameObject);
        int listSize = targetList.Count;
        for(int i = 1; i <= listSize; i++)
        {
            if(targetList[listSize-i] == null)
            {
                targetList.RemoveAt(listSize - i);
            }
        }
        //Debug.Log(targetList.Count + "Veridica Aicia");
        targetListPrime = targetList;
    }

    //The attack sequence used in the update method for the movement of the bullet
    void Attack(List<Transform> targets)
    {
        if (targets.Count == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Debug.Log(transform.position);
            transform.position = Vector3.MoveTowards(transform.position, targets[0].position + new Vector3(0,3.5f,0), bulletSpeed* Time.deltaTime);

            if (transform.position == targets[0].position + new Vector3(0,3.5f,0))
            {
                targets.RemoveAt(0);
                //Debug.Log(targets.Count);
            }

        }
    }

    public void DamageModifier()
    {
        damage /= 2;
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            enemyHealth.IncDamage(damage);
            DamageModifier();
        }

    }
}
