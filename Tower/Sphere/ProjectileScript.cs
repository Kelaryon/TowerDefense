using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour,IProjectileInterface
{
    Vector3 towerPos;
    [SerializeField] float speed;
    SphereTower spTower;
    Transform fixedTarget;
    bool tracking = false;

    public void Setup(Vector3 vic, SphereTower spTowerLink)
    {
        spTower = spTowerLink;
        towerPos = vic;
    }
    void Rotate(Vector3 towerPoz)
    {
        transform.RotateAround(towerPoz, Vector3.up, 120 * Time.deltaTime);
    }
    void FindTarget()
    {
        if (fixedTarget == null)
        {
            Rotate(towerPos);
            Transform target = TargetLocatorPrime.FindClosestTarget(towerPos);
            if (target != null)
            {
                float targetDistance = Vector3.Distance(towerPos, target.position);
                if (targetDistance < spTower.range)
                {
                    fixedTarget = target;
                }
            }
        }
        if(fixedTarget != null && tracking == false)
        {
            tracking = true;
        }
    }
    void Update()
    {
        if (!tracking)
        {
            FindTarget();
            CheckAndDestroy();
        }
        Attack();

    }

    public void DamageModifier()
    {
        Destroy(this.gameObject);
        spTower.ReduceSphereNumber();
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("am lovit");
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.IncDamage(spTower.damage);
            DamageModifier();
        }
    }
    void Attack()
    {
        if(tracking == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, fixedTarget.position + new Vector3(0, 3.5f, 0), speed * Time.deltaTime);
            speed += 0.1f;
            if (fixedTarget.gameObject.activeSelf == false)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void CheckAndDestroy()
    {
        if (spTower == null)
        {
            Destroy(this.gameObject);
        }
    }
}
