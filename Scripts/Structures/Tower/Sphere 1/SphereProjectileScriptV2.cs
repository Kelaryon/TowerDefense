using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereProjectileScriptV2 : MonoBehaviour
{
    [SerializeField] float speed;
    SphereTowerV2 spTower;
    Enemy fixedTarget;
    bool tracking = false;
    Vector3 towerPos;

    public void Setup(Vector3 vic, SphereTowerV2 spTowerLink)
    {
        spTower = spTowerLink;
        towerPos = vic;
    }
    void Rotate(Vector3 towerPoz)
    {
        transform.RotateAround(towerPoz, Vector3.up, 240 * Time.deltaTime);
    }
    void Update()
    {
        RotateOrAttack();
    }
    void Attack()
    {
            transform.position = Vector3.MoveTowards(transform.position, fixedTarget.transform.position + new Vector3(3, 3.5f, 3), speed * Time.deltaTime);
            speed += 0.1f;
            if (fixedTarget.gameObject.activeSelf == false)
            {
                Destroy(this.gameObject);
            }
            if(this.transform.position.x == fixedTarget.transform.position.x && this.transform.position.z == fixedTarget.transform.position.z)
        {
            fixedTarget.IncDamage(50f);
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
        fixedTarget = enemy;
        tracking = !tracking;
    }

}
