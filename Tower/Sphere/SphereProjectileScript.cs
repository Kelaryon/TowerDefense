using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereProjectileScript : MonoBehaviour
{
    [SerializeField] float speed;
    SphereTower spTower;
    Enemy enemy;
    bool tracking = false;
    Vector3 towerPos;

    public void Setup(Vector3 vic, SphereTower spTowerLink)
    {
        spTower = spTowerLink;
        towerPos = vic;
    }
    void Rotate(Vector3 towerPoz)
    {
        transform.RotateAround(towerPoz, Vector3.up, 120 * Time.deltaTime);
    }
    //void FindTarget()
    //{
    //    if (fixedTarget == null)
    //    {
    //        Rotate(towerPos);
    //        Transform target = spTower.tLocator.FindClosestTarget(towerPos);
    //        if (target != null)
    //        {
    //            float targetDistance = Vector3.Distance(towerPos, target.position);
    //            if (targetDistance < spTower.range)
    //            {
    //                fixedTarget = target;
    //            }
    //        }
    //    }
    //    if(fixedTarget != null && tracking == false)
    //    {
    //        tracking = true;
    //    }
    //}
    void Update()
    {
        RotateOrAttack();
    }
    void Attack()
    {
        try
        {
            //if (enemy.gameObject.activeSelf == false || enemy == null)
            //{
            //    Destroy(this.gameObject);
            //}

                transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position + new Vector3(0, 3.5f, 0), speed * Time.deltaTime);
                speed += 0.1f;


                if (this.transform.position.x == enemy.transform.position.x && this.transform.position.z == enemy.transform.position.z)
                {
                    if (spTower.GetStatusSelected() != null)
                    {
                        enemy.AddDebuff(spTower.GetStatusSelected().InitializeBuff(enemy.gameObject));
                    }
                    enemy.eHealth.IncDamage(50f);
                    Destroy(this.gameObject);
                }

        }
        catch(MissingReferenceException) {
            Destroy(this.gameObject);
        }
    }
    void RotateOrAttack()
    {
        if (tracking == false)
        {
            Rotate(towerPos);
        }
        else
        {
            Attack();
        }
    }
    public void SetTracking(Enemy enemy)
    {
        this.enemy = enemy;
        tracking = !tracking;
    }

}
